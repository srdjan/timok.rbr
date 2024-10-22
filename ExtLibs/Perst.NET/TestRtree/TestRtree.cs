using System;
using Perst;
using System.Diagnostics;

class SpatialObject : Persistent 
{ 
    public Rectangle rect;
}

public class TestRtree : Persistent { 
    SpatialIndex index;
    const int nObjectsInTree = 1000;
    const int nIterations = 100000;

    public static void Main(String[] args) { 
        Storage db = StorageFactory.Instance.CreateStorage();
        SpatialObject so;
        Rectangle r;
        DateTime start = DateTime.Now;
	db.Open("testrtree.dbs");
        TestRtree root = (TestRtree)db.Root;
        if (root == null) { 
            root = new TestRtree();
            root.index = db.CreateSpatialIndex();
            db.Root = root;
        }

        Rectangle[] rectangles = new Rectangle[nObjectsInTree];
        long key = 1999;
        for (int i = 0; i < nIterations; i++) { 
            int j = i % nObjectsInTree;
            if (i >= nObjectsInTree) { 
                r = rectangles[j];
                IPersistent[] sos = root.index.Get(r);
                IPersistent po = null;
                int n = 0;
                for (int k = 0; k < sos.Length; k++) { 
                    so = (SpatialObject)sos[k];
                    if (r.Equals(so.rect)) { 
                        po = so;
                    } else { 
                        Debug.Assert(r.Intersects(so.rect));
                    }
                }    
                Debug.Assert(po != null);
                for (int k = 0; k < nObjectsInTree; k++) { 
                    if (r.Intersects(rectangles[k])) {
                        n += 1;
                    }
                }
                Debug.Assert(n == sos.Length);
                root.index.Remove(r, po);
                po.Deallocate();
            }
            key = (3141592621L*key + 2718281829L) % 1000000007L;
            int top = (int)(key % 1000);
            int left = (int)(key / 1000 % 1000);            
            key = (3141592621L*key + 2718281829L) % 1000000007L;
            int bottom = top + (int)(key % 100);
            int right = left + (int)(key / 100 % 100);
            so = new SpatialObject();
            r = new Rectangle(top, left, bottom, right);
            so.rect = r;
            rectangles[j] = r;
            root.index.Put(r, so);

            if (i % 100 == 0) { 
                Console.Write("Iteration " + i + "\r");
                db.Commit();
            }
        }        
        root.index.Clear();
        Console.WriteLine();
        Console.WriteLine("Elapsed time " + (DateTime.Now - start));
        db.Close();
    }
}
