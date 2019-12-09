Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.IO.Compression
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Namespace DotNet.Utilities
    ''' <summary>
    ''' Http连接操作帮助类
    ''' </summary>
    Public Class HttpHelper

#Region "预定义方变量"
        '默认的编码
        Private encoding As Encoding = Encoding.[Default]
        'Post数据编码
        Private postencoding As Encoding = Encoding.[Default]
        'HttpWebRequest对象用来发起请求
        Private request As HttpWebRequest = Nothing
        '获取影响流的数据对象
        Private response As HttpWebResponse = Nothing
        '设置本地的出口ip和端口
        Private _IPEndPoint As IPEndPoint = Nothing
#End Region

#Region "Public"

        ''' <summary>
        ''' 根据相传入的数据，得到相应页面数据
        ''' </summary>
        ''' <param name="item">参数类对象</param>
        ''' <returns>返回HttpResult类型</returns>
        Public Function GetHtml(item As HttpItem) As HttpResult
            '返回参数
            Dim result As New HttpResult()
            Try
                '准备参数
                SetRequest(item)
            Catch ex As Exception
                result.Cookie = String.Empty
                result.Header = Nothing
                result.Html = ex.Message
                result.StatusDescription = "配置参数时出错：" + ex.Message
                '配置参数时出错

                Return result
            End Try
            Try
                '请求数据
                response = DirectCast(request.GetResponse(), HttpWebResponse)
                Using response
                    GetData(item, result)
                End Using
            Catch ex As WebException

                If ex.Response IsNot Nothing Then
                    response = DirectCast(ex.Response, HttpWebResponse)
                    Using response
                        GetData(item, result)
                    End Using
                Else
                    result.Html = ex.Message
                End If
            Catch ex As Exception

                result.Html = ex.Message
            End Try
            If item.IsToLower Then
                result.Html = result.Html.ToLower()
            End If
            Return result
        End Function
#End Region

#Region "GetData"

        ''' <summary>
        ''' 获取数据的并解析的方法
        ''' </summary>
        ''' <param name="item"></param>
        ''' <param name="result"></param>
        Private Sub GetData(item As HttpItem, result As HttpResult)
            '#Region "base"
            '获取StatusCode
            result.StatusCode = response.StatusCode
            '获取StatusDescription
            result.StatusDescription = response.StatusDescription
            '获取最后访问的URl
            result.ResponseUri = response.ResponseUri.ToString()
            '获取Headers
            result.Header = response.Headers
            '获取CookieCollection
            If response.Cookies IsNot Nothing Then
                result.CookieCollection = response.Cookies
            End If
            '获取set-cookie
            If response.Headers("set-cookie") IsNot Nothing Then
                result.Cookie = response.Headers("set-cookie")
            End If
            '#End Region

            '#Region "byte"
            '处理网页Byte
            Dim ResponseByte As Byte() = GetByte()
            '#End Region

            '#Region "Html"
            If ResponseByte IsNot Nothing AndAlso ResponseByte.Length > 0 Then
                '设置编码
                SetEncoding(item, result, ResponseByte)
                '得到返回的HTML
                result.Html = encoding.GetString(ResponseByte)
            Else
                '没有返回任何Html代码
                result.Html = String.Empty
            End If
            '#End Region
        End Sub
        ''' <summary>
        ''' 设置编码
        ''' </summary>
        ''' <param name="item">HttpItem</param>
        ''' <param name="result">HttpResult</param>
        ''' <param name="ResponseByte">byte[]</param>
        Private Sub SetEncoding(item As HttpItem, result As HttpResult, ResponseByte As Byte())
            '是否返回Byte类型数据
            If item.ResultType = ResultType.[Byte] Then
                result.ResultByte = ResponseByte
            End If
            '从这里开始我们要无视编码了
            If encoding Is Nothing Then
                Dim meta As Match = Regex.Match(Encoding.[Default].GetString(ResponseByte), "<meta[^<]*charset=([^<]*)[""']", RegexOptions.IgnoreCase)
                Dim c As String = String.Empty
                If meta IsNot Nothing AndAlso meta.Groups.Count > 0 Then
                    c = meta.Groups(1).Value.ToLower().Trim()
                End If
                If c.Length > 2 Then
                    Try
                        encoding = Encoding.GetEncoding(c.Replace("""", String.Empty).Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim())
                    Catch

                        If String.IsNullOrEmpty(response.CharacterSet) Then
                            encoding = Encoding.UTF8
                        Else
                            encoding = Encoding.GetEncoding(response.CharacterSet)
                        End If
                    End Try
                Else
                    If String.IsNullOrEmpty(response.CharacterSet) Then
                        encoding = Encoding.UTF8
                    Else
                        encoding = Encoding.GetEncoding(response.CharacterSet)
                    End If
                End If
            End If
        End Sub
        ''' <summary>
        ''' 提取网页Byte
        ''' </summary>
        ''' <returns></returns>
        Private Function GetByte() As Byte()
            Dim ResponseByte As Byte() = Nothing
            Dim _stream As New MemoryStream()

            'GZIIP处理
            If response.ContentEncoding IsNot Nothing AndAlso response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase) Then
                '开始读取流并设置编码方式
                _stream = GetMemoryStream(New GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
            Else
                '开始读取流并设置编码方式
                _stream = GetMemoryStream(response.GetResponseStream())
            End If
            '获取Byte
            ResponseByte = _stream.ToArray()
            _stream.Close()
            Return ResponseByte
        End Function

        ''' <summary>
        ''' 4.0以下.net版本取数据使用
        ''' </summary>
        ''' <param name="streamResponse">流</param>
        Private Function GetMemoryStream(streamResponse As Stream) As MemoryStream
            Dim _stream As New MemoryStream()
            Dim Length As Integer = 256
            Dim buffer As [Byte]() = New [Byte](Length - 1) {}
            Dim bytesRead As Integer = streamResponse.Read(buffer, 0, Length)
            While bytesRead > 0
                _stream.Write(buffer, 0, bytesRead)
                bytesRead = streamResponse.Read(buffer, 0, Length)
            End While
            Return _stream
        End Function
#End Region

#Region "SetRequest"

        ''' <summary>
        ''' 为请求准备参数
        ''' </summary>
        '''<param name="item">参数列表</param>
        Private Sub SetRequest(item As HttpItem)
            ' 验证证书
            SetCer(item)
            If item.IPEndPoint IsNot Nothing Then
                _IPEndPoint = item.IPEndPoint
                '设置本地的出口ip和端口
                request.ServicePoint.BindIPEndPointDelegate = New BindIPEndPoint(AddressOf BindIPEndPointCallback)
            End If
            '设置Header参数
            If item.Header IsNot Nothing AndAlso item.Header.Count > 0 Then
                For Each key As String In item.Header.AllKeys
                    request.Headers.Add(key, item.Header(key))
                Next
            End If
            ' 设置代理
            SetProxy(item)
            If item.ProtocolVersion IsNot Nothing Then
                request.ProtocolVersion = item.ProtocolVersion
            End If
            request.ServicePoint.Expect100Continue = item.Expect100Continue
            '请求方式Get或者Post
            request.Method = item.Method
            request.Timeout = item.Timeout
            request.KeepAlive = item.KeepAlive
            request.ReadWriteTimeout = item.ReadWriteTimeout
            If item.IfModifiedSince IsNot Nothing Then
                request.IfModifiedSince = Convert.ToDateTime(item.IfModifiedSince)
            End If
            'Accept
            request.Accept = item.Accept
            'ContentType返回类型
            request.ContentType = item.ContentType
            'UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            request.UserAgent = item.UserAgent
            ' 编码
            encoding = item.Encoding
            '设置安全凭证
            request.Credentials = item.ICredentials
            '设置Cookie
            SetCookie(item)
            '来源地址
            request.Referer = item.Referer
            '是否执行跳转功能
            request.AllowAutoRedirect = item.Allowautoredirect
            If item.MaximumAutomaticRedirections > 0 Then
                request.MaximumAutomaticRedirections = item.MaximumAutomaticRedirections
            End If
            '设置Post数据
            SetPostData(item)
            '设置最大连接
            If item.Connectionlimit > 0 Then
                request.ServicePoint.ConnectionLimit = item.Connectionlimit
            End If
        End Sub
        ''' <summary>
        ''' 设置证书
        ''' </summary>
        ''' <param name="item"></param>
        Private Sub SetCer(item As HttpItem)
            If Not String.IsNullOrEmpty(item.CerPath) Then
                '这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf CheckValidationResult)
                '初始化对像，并设置请求的URL地址
                request = DirectCast(WebRequest.Create(item.URL), HttpWebRequest)
                SetCerList(item)
                '将证书添加到请求里
                request.ClientCertificates.Add(New X509Certificate(item.CerPath))
            Else
                '初始化对像，并设置请求的URL地址
                request = DirectCast(WebRequest.Create(item.URL), HttpWebRequest)
                SetCerList(item)
            End If
        End Sub
        ''' <summary>
        ''' 设置多个证书
        ''' </summary>
        ''' <param name="item"></param>
        Private Sub SetCerList(item As HttpItem)
            If item.ClentCertificates IsNot Nothing AndAlso item.ClentCertificates.Count > 0 Then
                For Each c As X509Certificate In item.ClentCertificates
                    request.ClientCertificates.Add(c)
                Next
            End If
        End Sub
        ''' <summary>
        ''' 设置Cookie
        ''' </summary>
        ''' <param name="item">Http参数</param>
        Private Sub SetCookie(item As HttpItem)
            If Not String.IsNullOrEmpty(item.Cookie) Then
                request.Headers(HttpRequestHeader.Cookie) = item.Cookie
            End If
            '设置CookieCollection
            If item.ResultCookieType = ResultCookieType.CookieCollection Then
                request.CookieContainer = New CookieContainer()
                If item.CookieCollection IsNot Nothing AndAlso item.CookieCollection.Count > 0 Then
                    request.CookieContainer.Add(item.CookieCollection)
                End If
            End If
        End Sub
        ''' <summary>
        ''' 设置Post数据
        ''' </summary>
        ''' <param name="item">Http参数</param>
        Private Sub SetPostData(item As HttpItem)
            '验证在得到结果时是否有传入数据
            If Not request.Method.Trim().ToLower().Contains("get") Then
                If item.PostEncoding IsNot Nothing Then
                    postencoding = item.PostEncoding
                End If
                Dim buffer As Byte() = Nothing
                '写入Byte类型
                If item.PostDataType = PostDataType.[Byte] AndAlso item.PostdataByte IsNot Nothing AndAlso item.PostdataByte.Length > 0 Then
                    '验证在得到结果时是否有传入数据
                    buffer = item.PostdataByte
                    '写入文件
                ElseIf item.PostDataType = PostDataType.FilePath AndAlso Not String.IsNullOrEmpty(item.Postdata) Then
                    Dim r As New StreamReader(item.Postdata, postencoding)
                    buffer = postencoding.GetBytes(r.ReadToEnd())
                    r.Close()
                    '写入字符串
                ElseIf Not String.IsNullOrEmpty(item.Postdata) Then
                    buffer = postencoding.GetBytes(item.Postdata)
                End If
                If buffer IsNot Nothing Then
                    request.ContentLength = buffer.Length
                    request.GetRequestStream().Write(buffer, 0, buffer.Length)
                End If
            End If
        End Sub
        ''' <summary>
        ''' 设置代理
        ''' </summary>
        ''' <param name="item">参数对象</param>
        Private Sub SetProxy(item As HttpItem)
            Dim isIeProxy As Boolean = False
            If Not String.IsNullOrEmpty(item.ProxyIp) Then
                isIeProxy = item.ProxyIp.ToLower().Contains("ieproxy")
            End If
            If Not String.IsNullOrEmpty(item.ProxyIp) AndAlso Not isIeProxy Then
                '设置代理服务器
                If item.ProxyIp.Contains(":") Then
                    Dim plist As String() = item.ProxyIp.Split(":"c)
                    Dim myProxy As New WebProxy(plist(0).Trim(), Convert.ToInt32(plist(1).Trim()))
                    '建议连接
                    myProxy.Credentials = New NetworkCredential(item.ProxyUserName, item.ProxyPwd)
                    '给当前请求对象
                    request.Proxy = myProxy
                Else
                    Dim myProxy As New WebProxy(item.ProxyIp, False)
                    '建议连接
                    myProxy.Credentials = New NetworkCredential(item.ProxyUserName, item.ProxyPwd)
                    '给当前请求对象
                    request.Proxy = myProxy
                End If
                '设置为IE代理
            ElseIf isIeProxy Then
            Else
                request.Proxy = item.WebProxy
            End If
        End Sub
#End Region

#Region "private main"
        ''' <summary>
        ''' 回调验证证书问题
        ''' </summary>
        ''' <param name="sender">流对象</param>
        ''' <param name="certificate">证书</param>
        ''' <param name="chain">X509Chain</param>
        ''' <param name="errors">SslPolicyErrors</param>
        ''' <returns>bool</returns>
        Private Function CheckValidationResult(sender As Object, certificate As X509Certificate, chain As X509Chain, errors As SslPolicyErrors) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 通过设置这个属性，可以在发出连接的时候绑定客户端发出连接所使用的IP地址。 
        ''' </summary>
        ''' <param name="servicePoint"></param>
        ''' <param name="remoteEndPoint"></param>
        ''' <param name="retryCount"></param>
        ''' <returns></returns>
        Private Function BindIPEndPointCallback(servicePoint As ServicePoint, remoteEndPoint As IPEndPoint, retryCount As Integer) As IPEndPoint
            Return _IPEndPoint
            '端口号
        End Function
#End Region
    End Class
    ''' <summary>
    ''' Http请求参考类
    ''' </summary>
    Public Class HttpItem
        Private _URL As String = String.Empty
        ''' <summary>
        ''' 请求URL必须填写
        ''' </summary>
        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set
                _URL = Value
            End Set
        End Property
        Private _Method As String = "GET"
        ''' <summary>
        ''' 请求方式默认为GET方式,当为POST方式时必须设置Postdata的值
        ''' </summary>
        Public Property Method() As String
            Get
                Return _Method
            End Get
            Set
                _Method = Value
            End Set
        End Property
        Private _Timeout As Integer = 100000
        ''' <summary>
        ''' 默认请求超时时间
        ''' </summary>
        Public Property Timeout() As Integer
            Get
                Return _Timeout
            End Get
            Set
                _Timeout = Value
            End Set
        End Property
        Private _ReadWriteTimeout As Integer = 30000
        ''' <summary>
        ''' 默认写入Post数据超时间
        ''' </summary>
        Public Property ReadWriteTimeout() As Integer
            Get
                Return _ReadWriteTimeout
            End Get
            Set
                _ReadWriteTimeout = Value
            End Set
        End Property
        Private _KeepAlive As [Boolean] = True
        ''' <summary>
        '''  获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接默认为true。
        ''' </summary>
        Public Property KeepAlive() As [Boolean]
            Get
                Return _KeepAlive
            End Get
            Set
                _KeepAlive = Value
            End Set
        End Property
        Private _Accept As String = "text/html, application/xhtml+xml, */*"
        ''' <summary>
        ''' 请求标头值 默认为text/html, application/xhtml+xml, */*
        ''' </summary>
        Public Property Accept() As String
            Get
                Return _Accept
            End Get
            Set
                _Accept = Value
            End Set
        End Property
        Private _ContentType As String = "text/html"
        ''' <summary>
        ''' 请求返回类型默认 text/html
        ''' </summary>
        Public Property ContentType() As String
            Get
                Return _ContentType
            End Get
            Set
                _ContentType = Value
            End Set
        End Property
        Private _UserAgent As String = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36"
        ''' <summary>
        ''' 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        ''' </summary>
        Public Property UserAgent() As String
            Get
                Return _UserAgent
            End Get
            Set
                _UserAgent = Value
            End Set
        End Property
        Private _Encoding As Encoding = Nothing
        ''' <summary>
        ''' 返回数据编码默认为NUll,可以自动识别,一般为utf-8,gbk,gb2312
        ''' </summary>
        Public Property Encoding() As Encoding
            Get
                Return _Encoding
            End Get
            Set
                _Encoding = Value
            End Set
        End Property
        Private _PostDataType As PostDataType = PostDataType.[String]
        ''' <summary>
        ''' Post的数据类型
        ''' </summary>
        Public Property PostDataType() As PostDataType
            Get
                Return _PostDataType
            End Get
            Set
                _PostDataType = Value
            End Set
        End Property
        Private _Postdata As String = String.Empty
        ''' <summary>
        ''' Post请求时要发送的字符串Post数据
        ''' </summary>
        Public Property Postdata() As String
            Get
                Return _Postdata
            End Get
            Set
                _Postdata = Value
            End Set
        End Property
        Private _PostdataByte As Byte() = Nothing
        ''' <summary>
        ''' Post请求时要发送的Byte类型的Post数据
        ''' </summary>
        Public Property PostdataByte() As Byte()
            Get
                Return _PostdataByte
            End Get
            Set
                _PostdataByte = Value
            End Set
        End Property
        Private _WebProxy As WebProxy
        ''' <summary>
        ''' 设置代理对象，不想使用IE默认配置就设置为Null，而且不要设置ProxyIp
        ''' </summary>
        Public Property WebProxy() As WebProxy
            Get
                Return _WebProxy
            End Get
            Set
                _WebProxy = Value
            End Set
        End Property

        Private m_cookiecollection As CookieCollection = Nothing
        ''' <summary>
        ''' Cookie对象集合
        ''' </summary>
        Public Property CookieCollection() As CookieCollection
            Get
                Return m_cookiecollection
            End Get
            Set
                m_cookiecollection = Value
            End Set
        End Property
        Private _Cookie As String = String.Empty
        ''' <summary>
        ''' 请求时的Cookie
        ''' </summary>
        Public Property Cookie() As String
            Get
                Return _Cookie
            End Get
            Set
                _Cookie = Value
            End Set
        End Property
        Private _Referer As String = String.Empty
        ''' <summary>
        ''' 来源地址，上次访问地址
        ''' </summary>
        Public Property Referer() As String
            Get
                Return _Referer
            End Get
            Set
                _Referer = Value
            End Set
        End Property
        Private _CerPath As String = String.Empty
        ''' <summary>
        ''' 证书绝对路径
        ''' </summary>
        Public Property CerPath() As String
            Get
                Return _CerPath
            End Get
            Set
                _CerPath = Value
            End Set
        End Property
        Private m_isToLower As [Boolean] = False
        ''' <summary>
        ''' 是否设置为全文小写，默认为不转化
        ''' </summary>
        Public Property IsToLower() As [Boolean]
            Get
                Return m_isToLower
            End Get
            Set
                m_isToLower = Value
            End Set
        End Property
        Private m_allowautoredirect As [Boolean] = False
        ''' <summary>
        ''' 支持跳转页面，查询结果将是跳转后的页面，默认是不跳转
        ''' </summary>
        Public Property Allowautoredirect() As [Boolean]
            Get
                Return m_allowautoredirect
            End Get
            Set
                m_allowautoredirect = Value
            End Set
        End Property
        Private m_connectionlimit As Integer = 1024
        ''' <summary>
        ''' 最大连接数
        ''' </summary>
        Public Property Connectionlimit() As Integer
            Get
                Return m_connectionlimit
            End Get
            Set
                m_connectionlimit = Value
            End Set
        End Property
        Private m_proxyusername As String = String.Empty
        ''' <summary>
        ''' 代理Proxy 服务器用户名
        ''' </summary>
        Public Property ProxyUserName() As String
            Get
                Return m_proxyusername
            End Get
            Set
                m_proxyusername = Value
            End Set
        End Property
        Private m_proxypwd As String = String.Empty
        ''' <summary>
        ''' 代理 服务器密码
        ''' </summary>
        Public Property ProxyPwd() As String
            Get
                Return m_proxypwd
            End Get
            Set
                m_proxypwd = Value
            End Set
        End Property
        Private m_proxyip As String = String.Empty
        ''' <summary>
        ''' 代理 服务IP ,如果要使用IE代理就设置为ieproxy
        ''' </summary>
        Public Property ProxyIp() As String
            Get
                Return m_proxyip
            End Get
            Set
                m_proxyip = Value
            End Set
        End Property
        Private m_resulttype As ResultType = ResultType.[String]
        ''' <summary>
        ''' 设置返回类型String和Byte
        ''' </summary>
        Public Property ResultType() As ResultType
            Get
                Return m_resulttype
            End Get
            Set
                m_resulttype = Value
            End Set
        End Property
        Private m_header As New WebHeaderCollection()
        ''' <summary>
        ''' header对象
        ''' </summary>
        Public Property Header() As WebHeaderCollection
            Get
                Return m_header
            End Get
            Set
                m_header = Value
            End Set
        End Property

        Private _ProtocolVersion As Version

        '''' <summary>
        '     获取或设置用于请求的 HTTP 版本。返回结果:用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11。
        '  ''' </summary>
        Public Property ProtocolVersion() As Version
            Get
                Return _ProtocolVersion
            End Get
            Set
                _ProtocolVersion = Value
            End Set
        End Property
        Private _expect100continue As [Boolean] = True
        ''' <summary>
        '''  获取或设置一个 System.Boolean 值，该值确定是否使用 100-Continue 行为。如果 POST 请求需要 100-Continue 响应，则为 true；否则为 false。默认值为 true。
        ''' </summary>
        Public Property Expect100Continue() As [Boolean]
            Get
                Return _expect100continue
            End Get
            Set
                _expect100continue = Value
            End Set
        End Property
        Private _ClentCertificates As X509CertificateCollection
        ''' <summary>
        ''' 设置509证书集合
        ''' </summary>
        Public Property ClentCertificates() As X509CertificateCollection
            Get
                Return _ClentCertificates
            End Get
            Set
                _ClentCertificates = Value
            End Set
        End Property
        Private _PostEncoding As Encoding
        ''' <summary>
        ''' 设置或获取Post参数编码,默认的为Default编码
        ''' </summary>
        Public Property PostEncoding() As Encoding
            Get
                Return _PostEncoding
            End Get
            Set
                _PostEncoding = Value
            End Set
        End Property
        Private _ResultCookieType As ResultCookieType = ResultCookieType.[String]
        ''' <summary>
        ''' Cookie返回类型,默认的是只返回字符串类型
        ''' </summary>
        Public Property ResultCookieType() As ResultCookieType
            Get
                Return _ResultCookieType
            End Get
            Set
                _ResultCookieType = Value
            End Set
        End Property

        Private _ICredentials As ICredentials = CredentialCache.DefaultCredentials
        ''' <summary>
        ''' 获取或设置请求的身份验证信息。
        ''' </summary>
        Public Property ICredentials() As ICredentials
            Get
                Return _ICredentials
            End Get
            Set
                _ICredentials = Value
            End Set
        End Property
        ''' <summary>
        ''' 设置请求将跟随的重定向的最大数目
        ''' </summary>
        Private _MaximumAutomaticRedirections As Integer

        Public Property MaximumAutomaticRedirections() As Integer
            Get
                Return _MaximumAutomaticRedirections
            End Get
            Set
                _MaximumAutomaticRedirections = Value
            End Set
        End Property

        Private _IfModifiedSince As System.Nullable(Of DateTime) = Nothing
        ''' <summary>
        ''' 获取和设置IfModifiedSince，默认为当前日期和时间
        ''' </summary>
        Public Property IfModifiedSince() As System.Nullable(Of DateTime)
            Get
                Return _IfModifiedSince
            End Get
            Set
                _IfModifiedSince = Value
            End Set
        End Property
#Region "ip-port"
        Private _IPEndPoint As IPEndPoint = Nothing
        ''' <summary>
        ''' 设置本地的出口ip和端口
        ''' </summary>]
        ''' <example>
        '''item.IPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.1"),80);
        ''' </example>
        Public Property IPEndPoint() As IPEndPoint
            Get
                Return _IPEndPoint
            End Get
            Set
                _IPEndPoint = Value
            End Set
        End Property
#End Region
    End Class
    ''' <summary>
    ''' Http返回参数类
    ''' </summary>
    Public Class HttpResult
        Private _Cookie As String
        ''' <summary>
        ''' Http请求返回的Cookie
        ''' </summary>
        Public Property Cookie() As String
            Get
                Return _Cookie
            End Get
            Set
                _Cookie = Value
            End Set
        End Property

        Private _CookieCollection As CookieCollection
        ''' <summary>
        ''' Cookie对象集合
        ''' </summary>
        Public Property CookieCollection() As CookieCollection
            Get
                Return _CookieCollection
            End Get
            Set
                _CookieCollection = Value
            End Set
        End Property
        Private _html As String = String.Empty
        ''' <summary>
        ''' 返回的String类型数据 只有ResultType.String时才返回数据，其它情况为空
        ''' </summary>
        Public Property Html() As String
            Get
                Return _html
            End Get
            Set
                _html = Value
            End Set
        End Property
        Private _ResultByte As Byte()
        ''' <summary>
        ''' 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        ''' </summary>
        Public Property ResultByte() As Byte()
            Get
                Return _ResultByte
            End Get
            Set
                _ResultByte = Value
            End Set
        End Property
        Private _Header As WebHeaderCollection
        ''' <summary>
        ''' header对象
        ''' </summary>
        Public Property Header() As WebHeaderCollection
            Get
                Return _Header
            End Get
            Set
                _Header = Value
            End Set
        End Property
        Private _StatusDescription As String
        ''' <summary>
        ''' 返回状态说明
        ''' </summary>
        Public Property StatusDescription() As String
            Get
                Return _StatusDescription
            End Get
            Set
                _StatusDescription = Value
            End Set
        End Property
        Private _StatusCode As HttpStatusCode
        ''' <summary>
        ''' 返回状态码,默认为OK
        ''' </summary>
        Public Property StatusCode() As HttpStatusCode
            Get
                Return _StatusCode
            End Get
            Set
                _StatusCode = Value
            End Set
        End Property
        ''' <summary>
        ''' 最后访问的URl
        ''' </summary>
        Public Property ResponseUri() As String
            Get
                Return m_ResponseUri
            End Get
            Set
                m_ResponseUri = Value
            End Set
        End Property
        Private m_ResponseUri As String
        ''' <summary>
        ''' 获取重定向的URl
        ''' </summary>
        Public ReadOnly Property RedirectUrl() As String
            Get
                Try
                    If Header IsNot Nothing AndAlso Header.Count > 0 Then
                        If Header("location") IsNot Nothing Then
                            Dim locationurl As String = Header("location").ToString()

                            If Not String.IsNullOrEmpty(locationurl) Then
                                Dim b As Boolean = locationurl.StartsWith("http://") OrElse locationurl.StartsWith("https://")
                                If Not b Then
                                    locationurl = New Uri(New Uri(ResponseUri), locationurl).AbsoluteUri
                                End If
                            End If
                            Return locationurl
                        End If
                    End If
                Catch

                End Try
                Return String.Empty
            End Get
        End Property
    End Class
    ''' <summary>
    ''' 返回类型
    ''' </summary>
    Public Enum ResultType
        ''' <summary>
        ''' 表示只返回字符串 只有Html有数据
        ''' </summary>
        [String]
        ''' <summary>
        ''' 表示返回字符串和字节流 ResultByte和Html都有数据返回
        ''' </summary>
        [Byte]
    End Enum
    ''' <summary>
    ''' Post的数据格式默认为string
    ''' </summary>
    Public Enum PostDataType
        ''' <summary>
        ''' 字符串类型，这时编码Encoding可不设置
        ''' </summary>
        [String]
        ''' <summary>
        ''' Byte类型，需要设置PostdataByte参数的值编码Encoding可设置为空
        ''' </summary>
        [Byte]
        ''' <summary>
        ''' 传文件，Postdata必须设置为文件的绝对路径，必须设置Encoding的值
        ''' </summary>
        FilePath
    End Enum
    ''' <summary>
    ''' Cookie返回类型
    ''' </summary>
    Public Enum ResultCookieType
        ''' <summary>
        ''' 只返回字符串类型的Cookie
        ''' </summary>
        [String]
        ''' <summary>
        ''' CookieCollection格式的Cookie集合同时也返回String类型的cookie
        ''' </summary>
        CookieCollection
    End Enum
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'===================================

