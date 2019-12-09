// 版权所有：
// 文 件  名：XmlHelper.cs
// 功能描述：xml操作类 2013-1-5 马山林 添加
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/18 16:55:57
// 修改描述：2013-1-6  添加读取xml文本功能
//           2014-1-18 插入节点与查询功能完善 
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;
using System.Xml;

namespace TimedTask
{
    /// <summary>
    /// xml操作类
    /// 2013-1-5 马山林 添加
    /// 修改记录：2013-1-6 添加读取xml文本功能
    /// </summary>
    public class XmlHelper
    {
        #region 公共变量
        protected XmlDocument xmlDoc;
        string xmlfilepath = string.Empty;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数，导入Xml文件
        /// </summary>
        /// <param name="xmlFile">文件路径或xml文件文本</param>
        public XmlHelper(string xmlFile)
        {
            try
            {
                xmlDoc = new XmlDocument();
                if ((xmlFile.Length > 50 ? xmlFile.Substring(0, 50) : xmlFile).Contains("version"))
                {
                    xmlDoc.LoadXml(xmlFile);
                }
                else
                {
                    xmlfilepath = xmlFile;
                    xmlDoc.Load(xmlFile);   //载入Xml文档
                }
            }
            catch (System.Exception ex)
            {
                //throw ex;
            }
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~XmlHelper()
        {

            xmlDoc = null;  //释放XmlDocument对象
        }

        #endregion

        #region 保存xml文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件虚拟路径</param>
        public void Save()
        {
            try
            {

                xmlDoc.Save(xmlfilepath);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询

        /// <summary>
        /// 查询指定节点的指定属性值
        /// </summary>
        /// <param name="strNode">节点名</param>
        /// <param name="strAttribute">属性名</param>
        /// <returns>属性值</returns>
        public string GetXmlNodeValue(string strNode, string strAttribute)
        {
            string strReturn = "";

            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                //获取节点的属性，并循环取出需要的属性值
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;

                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == strAttribute)
                        strReturn = xmlAttr.Item(i).Value;
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }

        /// <summary>
        /// 属性查询，返回属性值
        /// </summary>
        /// <param name="XmlPathNode">属性所在的节点</param>
        /// <param name="Attrib">属性</param>
        /// <returns></returns>
        public string SelectAttrib(string XmlPathNode, string Attrib)
        {

            string _strNode = "";
            try
            {
                _strNode = xmlDoc.SelectSingleNode(XmlPathNode).Attributes[Attrib].Value;
            }
            catch
            { }
            return _strNode;
        }

        /// <summary>
        /// 节点查询，返回节点值
        /// </summary>
        /// <param name="XmlPathNode">节点的路径 如:Data/ArticleInfo/ArticleTitle</param>
        /// <returns></returns>
        public string SelectNodeText(string XmlPathNode)
        {
            string _nodeTxt = xmlDoc.SelectSingleNode(XmlPathNode).InnerText;
            if (_nodeTxt == null || _nodeTxt == "")
                return "";
            else
                return _nodeTxt;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="MainNode">属性所在节点</param>
        /// <param name="Attrib">属性名</param>
        /// <param name="AttribContent">属性值</param>
        /// <returns></returns>
        public bool InsertAttrib(string MainNode, string Attrib, string AttribContent)
        {
            try
            {
                XmlElement objNode = (XmlElement)xmlDoc.SelectSingleNode(MainNode); //强制转化成XmlElement对象
                objNode.SetAttribute(Attrib, AttribContent);    //XmlElement对象添加属性方法     
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 根据id获得XmlElement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XmlElement GetElementById(string id)
        {
            return xmlDoc.GetElementById(id);
        }
        /// <summary>
        /// 根据id获得节点值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetElementInnerTextById(string id)
        {
            XmlElement ele = xmlDoc.GetElementById(id);
            if (ele != null)
                return xmlDoc.GetElementById(id).InnerText;
            return "";
        }
        /// <summary>
        /// 根据节点名获得子节点列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNodeList GetElementsByTagName(string name)
        {
            return xmlDoc.GetElementsByTagName(name);
        }
        /// <summary>
        /// 节点属性获得第一个子节点
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public XmlElement GetElementsByNameAttribute(string name, string attributeName, string attributeValue)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(name);
            if (xmlList != null && xmlList.Count > 0)
            {
                foreach (XmlElement item in xmlList)
                {
                    try
                    {
                        if (item.Attributes[attributeName].Value == attributeValue)
                        {
                            return item;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据节点名获得第一个子节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNode GetElementByTagName(string name)
        {
            XmlNodeList nl = xmlDoc.GetElementsByTagName(name);
            if (nl != null && nl.Count > 0)
                return nl[0];
            return null;
        }

        /// <summary>
        /// 根据节点名获得节点列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNodeList GetNodesByTagName(string name)
        {
            XmlNodeList nl = xmlDoc.GetElementsByTagName(name);
            return nl;
        }

        /// <summary>
        /// 根据节点名获得第一个子节点的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetElementByTagNameString(string name)
        {
            XmlNodeList nl = xmlDoc.GetElementsByTagName(name);
            if (nl != null && nl.Count > 0)
                return nl[0].InnerText;
            return null;
        }

        #endregion

        #region 改
        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="xPath">节点名</param>
        /// <param name="xmlNodeValue">节点值</param>
        public void SetXmlNodeValue(string xPath, string xmlNodeValue)
        {
            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xPath);
                //设置节点值
                xmlNode.InnerText = xmlNodeValue;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 设置节点属性值 
        /// </summary>
        /// <param name="xPath">节点名XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// <param name="xmlNodeAttribute">属性名</param>
        /// <param name="xmlNodeAttributeValue">属性值</param>
        public void SetXmlNodeAttributeValue(string xPath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xPath);
                xmlNode.Attributes[xmlNodeAttribute].Value = xmlNodeAttributeValue;
                xmlDoc.Save(this.xmlfilepath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 更新N个节点值
        /// </summary>
        /// <param name="XmlParentNode">父节点</param>
        /// <param name="XmlNode">子节点</param>
        /// <param name="NodeContent">子节点内容</param>
        /// <returns></returns>
        public bool SetXmlNodesValue(string XmlParentNode, string[] XmlNode, string[] NodeContent)
        {

            try
            {
                //根据节点数组循环修改节点值
                for (int i = 0; i < XmlNode.Length; i++)
                {
                    xmlDoc.SelectSingleNode(XmlParentNode + "/" + XmlNode[i]).InnerText = NodeContent[i];
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id设置节点值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetElementInnerTextById(string id, string value)
        {
            XmlElement ele = xmlDoc.GetElementById(id);
            if (ele != null)
                xmlDoc.GetElementById(id).InnerText = value;
        }
        #endregion

        #region 增
        /// <summary>
        /// 插入一个节点，带N个子节点
        /// </summary>
        /// <param name="mainNode">插入节点的父节点</param>
        /// <param name="childNode">插入节点的元素名</param>
        /// <param name="element">插入节点的子节点名数组</param>
        /// <param name="content">插入节点的子节点内容数组</param>
        /// <returns></returns>
        public bool InsertNode(string mainNode, string childNode, string[] element, string[] content)
        {
            try
            {
                XmlNode objRootNode = xmlDoc.SelectSingleNode(mainNode);    //声明XmlNode对象
                XmlElement objChildNode = xmlDoc.CreateElement(childNode);  //创建XmlElement对象
                objRootNode.AppendChild(objChildNode);
                for (int i = 0; i < element.Length; i++)    //循环插入节点元素
                {
                    XmlElement objElement = xmlDoc.CreateElement(element[i]);
                    objElement.InnerText = content[i];
                    objChildNode.AppendChild(objElement);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 插入一个节点，带N个属性
        /// </summary>
        /// <param name="mainNode">插入节点的父节点</param>
        /// <param name="nodeNode">插入节点名</param>
        /// <param name="nodeValue">节点值</param>
        /// <param name="dic">属性</param>
        /// <returns></returns>
        public bool InsertNode(string mainNode, string nodeNode, string nodeValue, Dictionary<String, String> dic)
        {
            try
            {
                XmlNode rootNode = xmlDoc.SelectSingleNode(mainNode);    //声明XmlNode对象
                XmlElement xmlElement = xmlDoc.CreateElement(nodeNode);  //创建XmlElement对象
                rootNode.AppendChild(xmlElement);
                if (dic != null && dic.Count > 0)
                {
                    foreach (KeyValuePair<String, String> k in dic)//属性
                    {
                        xmlElement.Attributes.Append(xmlDoc.CreateAttribute(k.Key));
                        xmlElement.Attributes[k.Key].InnerText = k.Value;
                    }
                    xmlElement.InnerText = nodeValue;
                }
                xmlDoc.Save(this.xmlfilepath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一个节点及其子节点
        /// </summary>
        /// <param name="Node">要删除的节点</param>
        /// <returns></returns>
        public bool DeleteNode(string Node)
        {
            try
            {
                //XmlNode的RemoveChild方法来删除节点及其所有子节点
                xmlDoc.SelectSingleNode(Node).ParentNode.RemoveChild(xmlDoc.SelectSingleNode(Node));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 根据属性值删除一个节点
        /// </summary>
        /// <param name="Node">要删除的节点</param>
        /// <returns></returns>
        public bool DeleteNodeNameAttribute(string name, string attributeName, string attributeValue)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(name);
            if (xmlList != null && xmlList.Count > 0)
            {
                foreach (XmlElement v in xmlList)
                {
                    try
                    {
                        if (v.Attributes[attributeName].Value == attributeValue)
                        {
                            v.ParentNode.RemoveChild(v);
                            xmlDoc.Save(this.xmlfilepath);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
