﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Linda.Entity;
using Linda.DAL.IServer;
using Linda.DAL;
using Easo.DAL;
 namespace Easo.Web.DBMS.MsNews
{
    public partial class Addnews : AdminPage
    {
        Tb_Easo_News Mnews = new Tb_Easo_News();//实体类

        DalTb_Easo_News News = new DalTb_Easo_News();//操作类

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtaddtime.Value = DateTime.Now.ToString();

                this.dropmenu.DataSource = DalTb_Easo_NewClass.CreateInstance().GetWebNewClassList((int)GlobalData.最新公告);
                this.dropmenu.DataBind();
                dropmenu.Items.Insert(0, new ListItem("请选择", ""));

                string action = CRequest.GetString("action");

                if (action == "add")
                {
                    this.msgtitle.Text = "信息发布"; 
                    
                    string classid = Utils.ChkSQL(CRequest.GetString("classid")); 

                    if (classid != "")
                    {
                        ListItem item = this.dropmenu.Items.FindByValue(classid);
                        if (item != null)
                        {
                            item.Selected = true;
                        } 

                    }

                }
                else
                {
                    int id = CRequest.GetInt("nid", 0);
                    if (id <= 0)
                        Jscript.DamicAlert();
                    else
                    { 

                        msgtitle.Text = "信息修改";

                        add_update.Text = "修改";

                        //设置新闻内容
                        Mnews = News.GetModel(id);
                        if (Mnews == null)
                            Jscript.DamicAlert();
                        else
                        {                            

                            ViewState["ID"] = id;

                            txtContent.Value = Mnews.Content;
                            txtitle.Text = Mnews.Title;
                            txt_Source.Text = Mnews.Source;
                            txtaddtime.Value = Mnews.Addtime.ToString();
                            txtcommendnumber.Text = Mnews.RecommendOrderNum.ToString();
                            txtlabel.Text = Mnews.Label;  

                            ListItem item = dropmenu.Items.FindByValue(Mnews.ClassId.ToString());
                            if (item != null)
                                item.Selected = true;

                            raifcommend.SelectedValue = Mnews.IfRecommend.ToString();
                            raiftop.SelectedValue = Mnews.IfTop.ToString();
                            txtopnumber.Text = Mnews.TopOrderNum.ToString();

                            ViewState["ID"] = Mnews.ID.ToString();

                            if (Mnews.Downfile != "")
                            {
                                lafile.Text = "<a href='" + Mnews.Downfile + "' target='_blank'>已上传文件,查看文件</a>";
                            }
                            else
                                lafile.Text = "没有上传文件";

                            ViewState["file"] = Mnews.Downfile;

                        }
                    }
                }

            }
        }

        protected void add_update_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                string action = CRequest.GetString("action");


                string title = Utils.ChkSQL(this.txtitle.Text.Trim());
                string content = txtContent.Value.Trim();

                Mnews.Title = title;
                Mnews.Content = content;  
                Mnews.ClassId =Utils.StrToInt( dropmenu.SelectedValue);
                if (dropmenu.SelectedValue == "")
                    Jscript.Alert("请选择类别", this);
                else
                {
                    Mnews.Addtime =Convert.ToDateTime( txtaddtime.Value.ToString());
                    Mnews.Content = txtContent.Value.Trim();
                    Mnews.IfRecommend = Utils.StrToInt(raifcommend.SelectedValue);
                    Mnews.IfTop = Utils.StrToInt(raiftop.SelectedValue);
                    Mnews.Label = txtlabel.Text.Trim();
                    Mnews.Source = txt_Source.Text.Trim();
                    Mnews.TopOrderNum = Utils.StrToInt(txtopnumber.Text.Trim());
                    Mnews.RecommendOrderNum = Utils.StrToInt(txtcommendnumber.Text.Trim());
                   
                    //上传文件
                    string s = "";

                    if (dropmenu.SelectedItem.Text.IndexOf("下载") != -1 || dropmenu.SelectedItem.Text.IndexOf("学术")!=-1)
                    {
                        

                        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
                        {
                            s = DoClass.UploadFile(FileUpload1.PostedFile, Global.default_filesize, Global.default_imgtype, Global.default_imgpath);

                            if (s == "-1")
                            {
                                Jscript.Alert("文件类型错误！", this);
                                return;
                            }
                            else if (s == "0")
                            {

                                Jscript.Alert("文件太大!", this);
                                return;
                            }
                            else
                                s = Global.default_imgpath + s;

                        }
                        else
                            s = "";
                    } 
                 
                    
                    if (action == "add")
                    {

                        Mnews.Downfile = s;

                       
                        //新闻添加
                        if (News.Add(Mnews)>0)
                        {
                            Jscript.WriteInfo("发布成功！");
                        }
                    }
                    else
                    {

                        if (ViewState["ID"] == null)
                        {
                            Jscript.WriteInfo("id为Null不能执行修改！");
                        }
                        else
                        {

                            Mnews.ID = int.Parse(ViewState["ID"].ToString());

                            if (s != "")
                                Mnews.Downfile = s;
                            else
                                Mnews.Downfile = ViewState["file"].ToString();
                            
                            //新闻修改
                            if (News.Update(Mnews)>0)
                            {
                                  Jscript.WriteInfo("信息修改成功");
                            }
                        }

                    }
                }
            } 

        }
    }
}
