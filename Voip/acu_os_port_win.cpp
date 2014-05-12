/*****************************************************************************\
*
* Product:  Aculab SDK
* Filename: acu_os_port_win.c
*
* Copyright (C)2003 Aculab Plc
*
* This file provides a library of functions that hide operating system specifics 
* behind a platform-independent interface.
*
* This file is provided to make the Aculab example code clearer.  It is an example
* of how to write code using Aculab's API libraries.  Customers are free to write
* their applications in any way they choose.
*
* PLEASE NOTE: This file is NOT SUPPORTED by Aculab.  Do not use it in your
* applications unless you understand it!
*
\*****************************************************************************/

#include "acu_type.h"
#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <conio.h>
#include <stdarg.h>
#include <process.h>

/* If the application is being built with TiNG, include the TiNG libraries */
#ifdef TiNGTYPE_WINNT
#include <smdrvr.h>
#endif

#include "acu_type.h"
#include "acu_os_port.h"

/*****************************************************************************\
*
* OS Implementation of opaque structures
*
\*****************************************************************************/
/* opaque structure to represent a critical section */
struct _ACU_OS_CRIT_SEC {
	CRITICAL_SECTION cs;
}; 

/* opaque structure to represent a thread */
struct _ACU_OS_THREAD {
	HANDLE		thread_handle;
	ACU_OS_BOOL	active;
	int			exit_code;
}; 

/* opaque structure to represent a wait object */
struct _ACU_SDK_WAIT_OBJECT {
	HANDLE		wait_object;	/* the OS handle to wait on */
	ACU_OS_BOOL	handle_is_copy;	/* has handle been cloned from another source? */
}; 

/* used to get data into a thread */
typedef struct _ACU_WIN_THREAD_PARMS {
	ACU_OS_THREAD_FN	func;
	ACU_OS_THREAD*		thread_data;
	void*				user_data;
	HANDLE				event;
} ACU_WIN_THREAD_PARMS;

/*****************************************************************************\
*
* Variables local to this module
*
\*****************************************************************************/
static HANDLE g_screen_handle;

/*****************************************************************************\
*
* Constants local to this module
*
\*****************************************************************************/
#define MAX_SCREEN_BUF_SIZE 80 /* maximum width of a string to be printed */

/*****************************************************************************\
*
* Local prototypes
*
\*****************************************************************************/
// Windows threads require a different calling convention to the standard one.  
//This function hides that fact.
//static unsigned int __stdcall spawn_thread(void* data); 

/*****************************************************************************\
*
* Synchronisation functions
*
\*****************************************************************************/
/* allocate a critical section */
ACU_OS_CRIT_SEC* acu_os_create_critical_section(void) {
	ACU_OS_CRIT_SEC* crit_sec = (ACU_OS_CRIT_SEC*) acu_os_alloc(sizeof(ACU_OS_CRIT_SEC));

	if (crit_sec != NULL) {
		InitializeCriticalSection(&crit_sec->cs);
	}
	return crit_sec;
}

/* free a critical section */
void acu_os_destroy_critical_section(ACU_OS_CRIT_SEC* crit_sec) {
	DeleteCriticalSection(&crit_sec->cs);
	acu_os_free(crit_sec);
}

/* lock a critical section */
ACU_OS_BOOL acu_os_lock_critical_section(ACU_OS_CRIT_SEC* crit_sec) {
	EnterCriticalSection(&crit_sec->cs);
	return ERR_ACU_OS_NO_ERROR;
}

/* unlock a critical section */
ACU_OS_BOOL acu_os_unlock_critical_section(ACU_OS_CRIT_SEC* crit_sec) {
	LeaveCriticalSection(&crit_sec->cs);
	return ERR_ACU_OS_NO_ERROR;
}

/*****************************************************************************\
*
* wait object functions 
*
\*****************************************************************************/
ACU_SDK_WAIT_OBJECT* acu_os_convert_wait_object( ACU_WAIT_OBJECT wait_object ) {
	ACU_SDK_WAIT_OBJECT* acu_wait_object = (ACU_SDK_WAIT_OBJECT*) acu_os_alloc( sizeof(ACU_SDK_WAIT_OBJECT) );
	if( acu_wait_object != NULL ) {
		acu_wait_object->wait_object = wait_object;
		acu_wait_object->handle_is_copy = 1;
	}

	return acu_wait_object;
}

/* create a new wait object */
ACU_SDK_WAIT_OBJECT* acu_os_create_wait_object(void) {
	ACU_SDK_WAIT_OBJECT* wait_object = (ACU_SDK_WAIT_OBJECT*) acu_os_alloc(sizeof(ACU_SDK_WAIT_OBJECT));

	if (wait_object != NULL) {
		wait_object->wait_object = CreateEvent(NULL,  FALSE, FALSE, NULL);

		// return NULL if the event failed to be allocated
		if (wait_object->wait_object == INVALID_HANDLE_VALUE) {
			acu_os_free(wait_object);
			wait_object = NULL;
		}
	}

	return wait_object;
}

#ifdef TiNGTYPE_WINNT
/* create a new wait object based on a Prosody event */
ACU_SDK_WAIT_OBJECT* acu_os_create_wait_object_from_prosody(tSMEventId prosody_event) {
	ACU_SDK_WAIT_OBJECT* wait_object = (ACU_SDK_WAIT_OBJECT*) acu_os_alloc(sizeof(ACU_SDK_WAIT_OBJECT));

	if (wait_object != NULL) {
		wait_object->wait_object = prosody_event;
		wait_object->handle_is_copy = ACU_OS_TRUE;
	}

	return wait_object;
}
#endif /* TiNGTYPE_WINNT */	

/* destroy a wait object */
void acu_os_destroy_wait_object(ACU_SDK_WAIT_OBJECT* wait_object) {
	/* if we cloned the wait object from an OS supplied object, don't close it */
	if (!wait_object->handle_is_copy) {
		CloseHandle(wait_object->wait_object);
	}

	acu_os_free(wait_object);
}

/* wait for a wait object */
ACU_OS_ERR acu_os_wait_for_wait_object(ACU_SDK_WAIT_OBJECT* wait_object, int timeout) {
	DWORD result;
	ACU_OS_ERR error;

	result = WaitForSingleObject(wait_object->wait_object, timeout);

	switch (result)	{
		/* wait object signalled */
	case WAIT_OBJECT_0:
		error = ERR_ACU_OS_NO_ERROR;
		break;

		/* wait object not signalled in the time */
	case WAIT_TIMEOUT:
		error = ERR_ACU_OS_TIMED_OUT;
		break;

	case WAIT_ABANDONED:
		error = ERR_ACU_OS_SIGNAL;
		break;

	default:
		error = ERR_ACU_OS_PARAMETER;
	}

	return error;
}

/* wait for a number of objects */
ACU_OS_ERR acu_os_wait_for_any_wait_object(unsigned int object_count, ACU_SDK_WAIT_OBJECT* objects[], ACU_OS_BOOL* signalled, int timeout) {
	ACU_OS_ERR _result = ERR_ACU_OS_NO_ERROR; 
	HANDLE* _handles = (HANDLE*) acu_os_alloc(sizeof(HANDLE) * object_count);

	//-- assume none of the wait objects is signalled
	memset(signalled, ACU_OS_FALSE, sizeof(ACU_OS_BOOL) * object_count);

	//-- copy the _handles into a contiguous array
	unsigned int _i;
	for (_i = 0; _i < object_count; _i++) {
		_handles[_i] = objects[_i]->wait_object;
	}

	DWORD _event = WaitForMultipleObjects(object_count, _handles, FALSE, timeout);

	//-- emulate Unix poll() semantics on Windows - WaitForMultipleObjects() only returns the first of the objects that is signalled.  
	//-- poll() returns all of the objects that are signalled when it is called
	if ((_event >= WAIT_OBJECT_0) && (_event < WAIT_OBJECT_0 + object_count)) {
		//-- mark the wait object that returned as signalled
		signalled[_event - WAIT_OBJECT_0] = ACU_OS_TRUE;

		//-- WaitForMultipleObjects() tells us the first of the objects that is signalled.  
		for (_i  = (_event - WAIT_OBJECT_0) + 1; _i < object_count; _i++) {
			//-- if the wait object has been signalled, mark it as such
			if (WaitForSingleObject(_handles[_i], 0) == WAIT_OBJECT_0) {
				signalled[_i] = ACU_OS_TRUE;
			}
		}
	}
	else if (_event == WAIT_TIMEOUT) {
		_result = ERR_ACU_OS_TIMED_OUT;
	}
	else {
		_result = ERR_ACU_OS_SIGNAL;
	}

	acu_os_free(_handles);
	return _result;
}

/* signal a wait object */
ACU_OS_ERR acu_os_signal_wait_object(ACU_SDK_WAIT_OBJECT* wait_object) {
	ACU_OS_ERR error = ERR_ACU_OS_NO_ERROR;
	DWORD result = SetEvent(wait_object->wait_object);

	/* check for failure */
	if (result == 0) {
		error = ERR_ACU_OS_PARAMETER;
	}

	return error;	
}

/* unsignal a wait object */
ACU_OS_ERR acu_os_unsignal_wait_object(ACU_SDK_WAIT_OBJECT* wait_object) {
	ACU_OS_ERR error = ERR_ACU_OS_NO_ERROR;
	DWORD result = ResetEvent(wait_object->wait_object);

	/* check for error */
	if (result == 0) {
		error = ERR_ACU_OS_PARAMETER;
	}

	return error;
}

/* sleep for a number of milliseconds */
ACU_OS_ERR acu_os_sleep(unsigned int time) {
	Sleep(time);
	return ERR_ACU_OS_NO_ERROR;
}

/*****************************************************************************\
*
* Memory functions
*
\*****************************************************************************/
/* allocate size bytes */
void* acu_os_alloc(size_t size) {
	return  calloc(1, size);
}

/* release memory allocated with acu_os_alloc() */
void acu_os_free(void* mem) {
	free(mem);
}

//Functions used by the memory management class

ACU_OS_CRIT_SEC* memman_create_cs(void) {
	ACU_OS_CRIT_SEC* crit_sec = (ACU_OS_CRIT_SEC*) calloc(1, sizeof(ACU_OS_CRIT_SEC) );

	if( crit_sec != NULL ) {
		InitializeCriticalSection( &crit_sec->cs );
	}

	return crit_sec;
}

void memman_free_cs(ACU_OS_CRIT_SEC* cs) {
	DeleteCriticalSection(&cs->cs);
	free(cs);
}

/*******************************************************************************\
*
*   Timok util methods -- TEMP
*
\*******************************************************************************/
bool wait_for_events(HANDLE pEvent1, HANDLE pEvent2) {
	HANDLE _events[2]; 
	_events[0] = pEvent1;
	_events[1] = pEvent2;
	DWORD _event = WaitForMultipleObjects(2, _events, FALSE, INFINITE);

	//-- stop_thread signaled
	if (_event == WAIT_OBJECT_0 + 0) {
		ResetEvent(_events[0]);
		return false; 
	} 
	
	//-- start_colection signaled
	if (_event == WAIT_OBJECT_0 + 1) {
		ResetEvent(_events[1]);
		return true;
	} 

	//TODO: log this - it is a problem!
	return false; 
}
