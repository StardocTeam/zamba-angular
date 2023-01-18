using System;
namespace zamba.collections
{
    public interface IPageList {
        void AddRange(System.Collections.ArrayList value);

        System.Collections.ArrayList home();        
        System.Collections.ArrayList next();
        System.Collections.ArrayList back();
        System.Collections.ArrayList end();
        System.Collections.ArrayList getCurrentPage();
        System.Collections.ArrayList getPage(int index);
        System.Collections.ArrayList this[int index] { get; }

        void clear();
        bool existThis(object value);
        
        int CurrentPageFirtItemIndex { get; }
        int CurrentPageIndex { get; }
        int CurrentPageLastItemIndex { get; }
                      
        int ItemsCount { get; }
        int PageCount { get; }
        int PageSize { get; }        
    }
}
