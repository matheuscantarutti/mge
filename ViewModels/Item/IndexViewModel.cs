using System.Collections;
using System.Collections.Generic;
using mge.Models.Item;

namespace mge.ViewModels.Item
{
    public class IndexViewModel
    {
        public ICollection<ItemEntity> Items { get; set; }
        public string Mensagem { get; set; }
        public string Error { get; set; }
    }
}