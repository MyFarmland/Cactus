
using System;
namespace Cactus.Model.Other
{
    [Serializable()]
    public class PageTurnModel
    {
        private int? _pageIndex;
        public int? PageIndex { 
            get{
                this._pageIndex = this._pageIndex.HasValue ? this._pageIndex.Value:1;
                return this._pageIndex;
            }
            set {
                if (value <= 0)
                {
                    this._pageIndex = 1;
                }
                else {
                    this._pageIndex = value;
                }
            }
        }
        private int _pageCount;
        public int PageCount { get {
            if (this._itemSize == 0) { return 0; }
            this._pageCount = ((this._countSize == 0 ? 1 : this._countSize) + this._itemSize - 1) / this._itemSize;
            return this._pageCount;
        }
            set { this._pageCount = value; }
        }
        private int _countSize;

        public int CountSize
        {
            get { return _countSize; }
            set { _countSize = value; }
        }

        private int _itemSize;
        public int ItemSize { get { return this._itemSize; } set { if (value <= 0) { this._itemSize = 0; } else { this._itemSize = value; } } }
    }
}
