using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1131437_BookListView
{
    public partial class frmBooks : Form
    {
        // 宣告共用陣列
        string[] b_name = { "三國演義", "西遊記", "唐詩三百首", "楚辭", "西廂記", "水滸傳", "紅樓夢", "牡丹亭" };
        string[] author = { "羅貫中", "吳承恩", "孫洙", "劉向", "王實甫", "施耐庵", "曹雪芹", "湯顯祖" };
        string[] kind = { "章回小說", "章回小說", "詩選", "詩歌", "戲曲", "章回小說", "章回小說", "戲曲" };
        public frmBooks()
        {
            InitializeComponent();
        }

        private void frmBooks_Load(object sender, EventArgs e)
        {
            cmbView.Items.Add("大圖示");
            cmbView.Items.Add("詳細資料");
            cmbView.Items.Add("小圖示");
            cmbView.Items.Add("清單");
            cmbView.Items.Add("大圖示加詳細資料");
            cmbView.SelectedIndex = 0; // 預設選取第一個項目

            lvwBooks.Columns.Add("書名", 100);
            lvwBooks.Columns.Add("作者", 60);
            lvwBooks.Columns.Add("類別", 60);

            lvwBooks.BeginUpdate(); // 暫停重繪，避免畫面閃爍

            for (int i = 0; i < b_name.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(b_name[i]);
                lvi.SubItems.Add(author[i]);
                lvi.SubItems.Add(kind[i]);
                lvwBooks.Items.Add(lvi);
                lvwBooks.Items[i].ImageIndex = i; // 指定影像的索引值
            }

            lvwBooks.EndUpdate(); // 恢復重繪
        }

        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbView.SelectedIndex)
            {
                case 0: lvwBooks.View = View.LargeIcon; break;
                case 1: lvwBooks.View = View.Details; break;
                case 2: lvwBooks.View = View.SmallIcon; break;
                case 3: lvwBooks.View = View.List; break;
                case 4: lvwBooks.View = View.Tile; break;
            }
        }

        private void lvwBooks_ItemActivate(object sender, EventArgs e)
        {
            // 取得選取項目的書名
            string strBookname = b_name[lvwBooks.SelectedIndices[0]];
            bool exist = lstBorrow.Items.Contains(strBookname);

            if (!exist) // 若選取的書名不存在借書清單中
            {
                DialogResult dr = MessageBox.Show("確定要借閱嗎?", strBookname, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    lstBorrow.Items.Add(strBookname);
                }
            }
        }
    }
}
