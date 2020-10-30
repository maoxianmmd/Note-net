using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerenlianxisheng.Models
{
    //母页模型
    public class IndexAsid
    {
        public List<AsidList> asid_lists { get; set; }
        public List<TypeBigLists> type_big_lists { get; set; }
    }

    public class AsidList
    {
        public string name { get; set; }
        public string type { get; set; }
        public AsidList Clone()
        {
            AsidList rs;
            rs = new AsidList();
            rs.name = this.name;
            rs.type = this.type;
            return rs;
        }
    }

    public class TypeBigLists
    {
        public string big { get; set; }
        public List<Small> small { get; set; }
        public TypeBigLists Clone()
        {
            TypeBigLists rs;
            rs = new TypeBigLists();
            rs.big = this.big;
            rs.small = this.small;
            return rs;
        }
    }

    public class Small
    {
        public string value { get; set; }
        public int key { get; set; }
        public Small Clone()
        {
            Small rs;
            rs = new Small();
            rs.value = this.value;
            rs.key = this.key;
            return rs;
        }
    }

    //子页模型
    public class JiluModel
    {
        public List<JiluNerong> tables { get; set; }
        public List<JiluAsid> types { get; set; }
    }

    public class JiluAsid
    {
        public string type_small { get; set; }
        public ArrayList mulu_small { get; set; }
        public int show { get; set; }
        public JiluAsid Clone()
        {
            JiluAsid rs;
            rs = new JiluAsid();
            rs.type_small = this.type_small;
            rs.mulu_small = this.mulu_small;
            rs.show = this.show;
            return rs;
        }
    }

    public class JiluNerong
    {
        public int id { get; set; }
        public string title { get; set; }
        public string neirong { get; set; }
        public string bigtitle { get; set; }
        public string smalltitle { get; set; }
        public JiluNerong Clone()
        {
            JiluNerong rs;
            rs = new JiluNerong();
            rs.id = this.id;
            rs.title = this.title;
            rs.neirong = this.neirong;
            rs.bigtitle = this.bigtitle;
            rs.smalltitle = this.smalltitle;
            return rs;
        }
    }
}
