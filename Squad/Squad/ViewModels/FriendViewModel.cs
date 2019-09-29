using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Squad
{
    public class FriendViewModel
    {
        ObservableCollection<Friend> Items { get; set; }
        public FriendViewModel()
        {
            Items = new ObservableCollection<Friend>(); // init. your items 
        }
    }
}
