//using System;
//using Timok.Rbr.Core.Config;

//namespace Timok.Rbr.Core {
//  public interface IChangedObject {
//    int Id { get; }
//    Type Type { get; }
//    void Invalidate();
//    bool IsEqualTo(IChangedObject pChangedObject);
//  }

//  //-- Base implementation -
//  [Serializable]
//  public abstract class ChangedObjectBase : IChangedObject {
//    protected TxType txType;

//    readonly Type type;
//    public Type Type { get { return type; } }

//    protected ChangedObjectBase(Type pChangedObjectType, TxType pTxType) {
//      type = pChangedObjectType;
//      txType = pTxType;
//    }

//    #region IChangedObject Members

//    public abstract int Id { get; }
//    public abstract void Invalidate();

//    public bool IsEqualTo(IChangedObject pChangedObject) {
//      return pChangedObject.Type == type && Id == pChangedObject.Id;
//    }

//    #endregion
//  }
//}