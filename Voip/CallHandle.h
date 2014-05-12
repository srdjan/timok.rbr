#pragma once

#include "common.h"

namespace Timok_IVR {
	ref class CallHandle {
	private:
		Card^ card;
		//IntPtr intptr_waithandle;

	public:
		AutoPtr<ACU_ALLOC_EVENT_QUEUE_PARMS> Event_queue;
		AutoPtr<ACU_QUEUE_WAIT_OBJECT_PARMS> Wait_object;
		ACU_CALL_HANDLE	Sip_handle;
		ACU_CALL_HANDLE	Acu_handle;

		CallHandle(Card^ pCard) : Event_queue(new ACU_ALLOC_EVENT_QUEUE_PARMS()),
									 Wait_object(new ACU_QUEUE_WAIT_OBJECT_PARMS()) {
			card = pCard;
			//intptr_waithandle = 0;
		}
		~CallHandle() {}

		virtual property HANDLE WaitObject { HANDLE get() { return Wait_object->wait_object; }	}		

		void CallHandle::Aloc(ACU_RESOURCE_ID pResourceId) {
			INIT_ACU_STRUCT(Event_queue.GetPointer());

			int _result = 0;
			if ((_result = acu_allocate_event_queue(Event_queue.GetPointer())) != 0) {
				throw gcnew Exception(String::Format("CallHandle.Aloc.acu_allocate_event_queue: {0}", _result));
			}

			if (card->IVRConfig->IsProsodyX) {
				ACU_QUEUE_PARMS	_port_queue;
				INIT_ACU_CL_STRUCT(&_port_queue);
				_port_queue.queue_id = Event_queue->queue_id;
				_port_queue.resource_id = pResourceId;
				if ((_result = call_set_port_default_handle_event_queue(&_port_queue)) != 0) {
					throw gcnew Exception(String::Format("CallHandle.Aloc.call_set_port_default_handle_event_queue resourceId: {0}", pResourceId));
				}
			}

			INIT_ACU_STRUCT(Wait_object.GetPointer());
			Wait_object->queue_id = Event_queue->queue_id;
			if ((_result = acu_get_event_queue_wait_object(Wait_object.GetPointer())) != 0) {
				throw gcnew Exception(String::Format("CallHandle.Aloc.acu_get_event_queue_wait_object: {0}", _result));
			}
		}

		void CallHandle::Free() {
			int	_result = 0;
			CAUSE_XPARMS _cause_xparms;
			INIT_ACU_STRUCT(&_cause_xparms);
			_cause_xparms.handle = Acu_handle;
			if ((_result = call_release(&_cause_xparms)) !=0)	{
				throw gcnew Exception(String::Format("CallHandle.Free, ERROR: {0}, call_release", _result));
			}

			ACU_FREE_EVENT_QUEUE_PARMS _free_event_parms;
			INIT_ACU_STRUCT(&_free_event_parms);
			_free_event_parms.queue_id = Event_queue->queue_id;
			if ((_result = acu_free_event_queue(&_free_event_parms)) != 0) {
				throw gcnew Exception(String::Format("CallHandle.Free, ERROR: {0}, acu_free_event_queue", _result));
			}
		}
	};
}
