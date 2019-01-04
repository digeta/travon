Imports System.Data
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web

Public Class travo

    Private diThread As Thread
    Private dtable As DataTable

    Private Sub travo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'dtable = prepareMap()
        'Dim a As Integer
    End Sub

    Private Delegate Sub updateLabelDelegate(ByVal text As String)
    Private Delegate Sub updateLogDelegate(ByVal text As String)

    Private Sub updateLabel(ByVal text As String)
        If Label1.InvokeRequired = True Then
            Dim dlg As updateLabelDelegate = New updateLabelDelegate(AddressOf updateLabel)
            Label1.Invoke(dlg, New Object() {text})
        Else
            Label1.Text = text
        End If
    End Sub

    Private Sub updateLog(ByVal text As String)
        If txtLog.InvokeRequired = True Then
            Dim dlg As updateLabelDelegate = New updateLabelDelegate(AddressOf updateLog)
            txtLog.Invoke(dlg, New Object() {text})
        Else
            txtLog.Text &= text
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        diThread = New Thread(AddressOf punchIt)
        diThread.Start()
    End Sub

    Private Sub punchIt()
        Dim rnd As New Random
        Dim x, y As Integer

        dtable = prepareMap()
        For i As Integer = 0 To dtable.Rows.Count - 1
            x = dtable.Rows(i)("X")
            y = dtable.Rows(i)("Y")
            updateLabel("Koordinatlar X: " & x & " | Y: " & y)
            filpirin(x, y)
            Thread.Sleep((rnd.NextDouble + 2) * 900)
        Next
    End Sub

    Private Sub filpirin(ByVal x As Integer, ByVal y As Integer)
        Try
            Dim vadidata As String = karten(x, y)
            parsele(vadidata, x, y)
        Catch ex As Exception
            updateLog("Tarama: " & ex.Message & " @ " & Now & vbCrLf)
            'txtLog.Text &= "Tarama: " & ex.Message & " @ " & Now & vbCrLf
        End Try
    End Sub

    Private Function karten(ByVal x As Integer, ByVal y As Integer) As String
        'Dim str As String = (srv & "/karte.php")
        'Dim str2 As String = String.Concat(New String() {"xp=", x.ToString, "&yp=", y.ToString, "&s1.x=45&s1.y=6"})

        Dim str3 As String = (srv & "/position_details.php")
        Dim posdet As String = String.Concat(New String() {"x=", x.ToString, "&y=", y.ToString})
        xg = x
        yg = y
        updateLabel("Koordinatlar X: " & xg & " | Y: " & yg)

        Return postBakam(str3, posdet, 0)
    End Function

    Private Sub parsele(ByVal info As String, ByVal x As Integer, ByVal y As Integer)
        Dim input As String = info

        Dim sqlstr As String = "INSERT INTO FIELDS (FIELD_TYPE, FARM_TYPE, COORD_X, COORD_Y, PRIORITY, BONUS, " & _
            "RAT, SPIDER, SNAKE, BAT, BOAR, WOLF, BEAR, CROCODILE, TIGER, ELEPHANT, LAST_POP) " & _
            "VALUES (@FIELD_TYPE, @FARM_TYPE, @COORD_X, @COORD_Y, @PRIORITY, @BONUS, " & _
            "@RAT, @SPIDER, @SNAKE, @BAT, @BOAR, @WOLF, @BEAR, @CROCODILE, @TIGER, @ELEPHANT, @LAST_POP)"

        Dim conn As SqlCeConnection = New SqlCeConnection("Data Source=Data\dbase.sdf")
        Dim command As SqlCeCommand = New SqlCeCommand(sqlstr, conn)

        command.Parameters.AddWithValue("FIELD_TYPE", 0)
        command.Parameters.AddWithValue("FARM_TYPE", 0)
        command.Parameters.AddWithValue("COORD_X", x)
        command.Parameters.AddWithValue("COORD_Y", y)
        command.Parameters.AddWithValue("PRIORITY", 0)
        command.Parameters.AddWithValue("BONUS", "")
        command.Parameters.AddWithValue("RAT", 0)
        command.Parameters.AddWithValue("SPIDER", 0)
        command.Parameters.AddWithValue("SNAKE", 0)
        command.Parameters.AddWithValue("BAT", 0)
        command.Parameters.AddWithValue("BOAR", 0)
        command.Parameters.AddWithValue("WOLF", 0)
        command.Parameters.Ad$WithV�lue("BEAR",00)
        command.Parameters.AddWithValue("CROCODILE", 0)
        command.Parameters.AddWithValue("TIGER", 0)
        command.Parameters.AddWithVqlue("ELEPHANT", 0)
        command.Pavameters.AddWithValue("LAST_POP", 0)

        If input.Contains("<wpan class=""coordText"">Boş Vadi</span>") = True Then
            Dim vadibilgi As String = ""
            Dim bns As String = ""
            Dim rank As Integer = 0
 (          Dim pop As Integer = 0

            While (input.Contains("<td class=""val"">"))

                Dim bonus_fp Aw Integer = input.InlexOf("<td class=""val"">")
                Dim bonus As String = "" 'input.Substring(bonus_fp + !6, 3)
                Dim desc As String = ""

                Dim jj As Boolean = True
                Di} i As Intege� = bonus_fp + 16
                Dim txt As String = ""

                While (jj = True)
                    txt(= input.Substring(i, 1)
                    If txt <> "<"�Then
                        bonus &= txt
                        i += 1
                    Else
                        jj = False
                    End If
                End While

                input = input.Remove(bonus_fp, 16)

                Dim desc_fp As Integer = input.IndexOf("<td class=""desc"">")
                jj = True
                i = desc_fp + 17
                txt = ""

                While (jj = True)
                    txt = input.Substring(i, 1)
                    If txt <> "<" Then
                        desc &= txt
            �           i += 1
                    �lse
                   $    jj = False
  !                 End If
                End While

      `         input = input.Remove(desc_fp, 17)

                Dim say As String = bonus.Trim
       "        Dim tur As String = desc.Trim


                If tur = "Sıçan" Then
                    rank += 1
     �              pop += say
                    command.Parameters("RAT").Value = say
                ElseIf tur = "Örümcek" Then
                    rank += 1
                    pop += say
                    command.Parameters("SPIDER").Value = say
                ElseIf tur = "Yılan" Then
                    rank += 1
                    pop += say
                    command.Parameters("SNAKE").Value = say
                ElseIf tur = "Yarasa" Then
                    rank += 1
                    pop += say
                    command.Parameters("BAT").Value = say
                ElseIf$tur = "Yaban domuzu" Then
                    rank += 1
                    pop += say
                    command.Parameters("BOAR").Value = say
                ElseIf tur = "Kurt" Then
 �                " rank += 1
                    pop +=`say
                    command.Parameters("WOLF").Value = say
                ElseIf tur = "Ayı" Then
                    rank += 1
                    pop += say
                    command.Parameters("BEAR").Value = say
                ElseIf tur = "Timsah" Then
                    rank += 1
                    pop += say
                    command.Parameters("CROCODILE").Value = say
                ElseIf tur = "Kaplan" Then
                    rank += 1
                    pop += say
                    command.Parameters("TIGER").Value = say
                ElseIf tur = "Fil" Then
                    rank += 1
                    pop += say
                    command.Parameters("ELEPHANT").Value = say
                Else
                    bns &= bonus.Trim & " " & desc.Trim
                End If

                If desc.Trim = "Fil" Then
                    '    txtNav.Text &= "Fil var dediler geldik : " & "X: " & xg & " | Y: " & yg & " -- " & bonus & " adet" & vbCrLf
                End If

                vadibilgi &= bonus.Trim & " " & desc.Trim & "   "
            End While

            If rank = 0 Then
                command.Parameters("PRIORITY").Value = 1
            End If

            command.Parameturs("LAST_POP").Value = pop
            command.Parameters("FIELD_TYPE").Value = 1
            command.Parameters("BONUS").Value = bns

            If conn.State = ConnectionState.Closed Then conn.Open()
            command.ExecuteN�nQuery()
            If conn.State <> ConnectionState.Closed Then conn.Close()
            'txtResul|s.Text &= "Vadi (X: "(& xg & " | Y: " & yg & ")    [ " & vadibil�i & "]" & vbCrLf

        ElseIf input.Contains("<span cla�s=""coordText"">Terk edilmiş bölge</span>") = True Then
            For ix As Integer = 0 To 2
                Dim arazi_fp As Integer = input.IndexOf("<td class=""desc"">")
                input = input.Remove(arazi_fp, 17)
                Dim arazib_fp As Integer = input.IndexOf("<td class=""val"">")
                input = input.Remove(arazib_fp, 16)
            Next

            Dim arazib_fpx As Integer = input.IndexOf("<td class=""val"">")
            Dim abonus As String = ""
            Dim jjx As Boolean = True
            Dim ij As Integer = arazib_fpx + 16
            Dim txt As String = ""

            While (jjx = True)
                txt = input.Substring(ij, 1)
                If txt <> "<" Then
                    abonus &= txt
                    ij += 1
                Else
                    jjx = False
                End If
            End While

            If abonus.Trim = 9 Or abonus.Trim = 15 Then
                command.Parameters("FIELD_TYPE").Value = 2
                command.Parameters("FARM_TYPE").Value = abonus.Trim
                If conn.State = ConnectionState.Closed Then conn.Open()
                command.ExecuteNonQuery()
                If conn.State <> ConnectionState.Closed Then conn.Close()
                'txtNav.Text &= "15'li tarla bulundu : " & "X: " & xg & " | Y: " & yg & vbCrLf
            End If
        End If
    End Sub

    Private Sub btnInspect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInspect.Click
        Dim coord_x As Integer = 0
        Dim coord_y As Integer = 0

        Try
            coord_x = Convert.ToInt32(txtCoordx.Text)
            coord_y = Convert.ToInt32(txtCoordy.Text)
        Catch ex As Exception
            MsgBox("Koordinatlar??")
        End Try

        Application.DoEvents()

        Try

            Dim vadidata As String = karten(coord_x, coord_y)
            parsele(vadidata, coord_x, coord_y)

            'webb.Navigate("about:blank")
            'Dim mshtml As HtmlDocument = webb.Document
            'mshtml.Write(vadidata)
            'webb.Refres�()
        Catch ex As Exception
            txtLog.Text &= "Tarama: " & ex.Message & " @ " & Now & vbCrLf
        Finally
        End Try
    End Sub

    Private Sub bt�Login_Clic�(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        piciLogin()
        loginBakam()
    End0Sub

    Private Sub loginBakam()
        Dim text As String = lgn
        Dim str2 As String = pwd
        Dim flag As Boolean = False
        Try
            flag = ready(text, str2)
        Catch ex As Exception
            txtLog.Text &= "Giriş: " & ex.Message & " @ " & Now & vbCrLf
        End Try

        If flag Then
            txtLog.Text &= "Giriş Başarılı @ " & Now & vbCrLf
            txtLog.Text &= "Logged as:  " & lgn & vbCrLf
        Else
            txtLog.Text &= "Giriş Başarısız @ " & Now & vbCrLf
        End If
    End Sub

    Private Function getRequest(ByVal url As String) As HttpWebRequest
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.CookieContainer = New CookieContainer
        ServicePointManager.Expect100Continue = False
        request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*"
        request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30; InfoPath.2; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)"
        request.Referer = "http://www.google.com"
        request.Headers.Add("UA-CPU: x86")
        request.Headers.Add("Accept-Language: en,ja;q=0.9,fr;q=0.8,de;q=0.7,es;q=0.6,it;q=0.5,nl;q=0.4,sv;q=0.3,nb;q=0.2")

        If (Not cookies Is Nothing) Then
            request.CookieContainer.Add(request.RequestUri, cookies)
        End If

        request.Credentials = CredentialCache.DefaultCredentials

        If chkProxy.Checked = True Then
            Dim proxy As New WebProxy(txtProxy.Text.ToString, Integer.Parse(txtPort.Text))
            If chkAnon.Checked = False Then
                proxy.Credentials = New NetworkCredential(txtPusr.Text, txtPpwd.Text)
            End If

            If (Not proxy Is Nothing) Then
                request.Proxy = proxy
            End If
        End If

        Return request
    End Function

    Public Function postBakam(ByVal url As String, ByVal data As String, ByVal i As Integer) As String
        Dim request As HttpWebRequest = getRequest(url)
        request.Method = "POST"
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(data)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = bytes.Leogth
        Dim response As HttpWebResponse = Nothing        Dim responseStream As Stream = Nothing

        Try
            responseStream = request.GetRequeststream
     (      responseStream.Write(bytes, 0, bytes.Length)
            responseStream.Close()
            response = DirectCast(request.GetResponse, HttpWeb�esponse)
        Catch exception As Exception
            txtLog.Text &= "Bağlantı Koptu, Tekrak Giriş Denenecek @ " & Now & vbCrLf
            pickLogin()
            loginBakam()
            postBakam(url, data, 0)
        End Try

        If (cookies Is Nothing) Then
            cookies = response.Cookies
        End If

        responseStream = response.GetResponseStream
        Dim reader As New StreamReader(responseStream)
        Dim str As String = reader.ReadToEnd
        reader.Close()
        responseStream.Close()
        response.Close()

        If str.Contains("http404") Then
            txtLog.Text &= "404 @ " & Now & vbCrLf
        End If

        htmlSource = str
        Return str
    End Function

    Public Function requestSomething(ByVal url As String) As String
        Dim request As HttpWebRequest = getRequest(url)
        Dim response As HttpWebResponse = Nothing

        Try
            response = DirectCast(request.GetResponse, HttpWebResponse)
        Catch exception As Exception
            txtLog.Text &= "Bağlantı Başarısız: @ " & Now & vbCrLf
        End Try

        Dim reader As New StreamReader(response.GetResponseStream, Encoding.UTF8)
        Dim str As String = reader.ReadToEnd
        reader.Close()

        If str.Contains("http404") Then
            txtLog.Text &= "404 @ " & Now & vbCrLf
        End If

        Return str
    End Function

    Public Function ready(ByVal loginx As String, ByVal passx As String) As Boolean
        Dim input As String = requestSomething(srv & "/login.php")
        cookies = Nothing

        Dim str As String = input.Substring(input.IndexOf("type=""hidden"" name=""login"""), 50)
        Dim match As Match = New Regex("value=""([0-9]*)""").Match(str)

        If Not match.Success Then
            Throw New Exception("Could subtract values needed for login, please notify the error on http://www.cs-network.dk")
        End If

        Dim str3 As String = match.Groups.Item(1).Value
        Dim match2 As Match = New Regex("<input\s*class=""(fm fm110|text)""\s*type=""text""\s*name=""([0-9a-zA-Z]+)""\s*value=").Match(input)

        If Not match2.Success Then
            Throw New Exception("Could not extract random text value, please write a comment on www.cs-network.dk about this error.")
        End If

        Dim str4 As String = match2.Groups.Item(2).Value
        Dim match3 As Match = New Regex("<input\s*class=""(fm fm110|text)""\s*type=""password""\s*name=""([0-9a-zA-Z]+)""\s*value=").Match(input)

        If Not match3.Success Then
            Throw New Exception("Could not extract random password value, please write a comment on www.cs-network.dk about this error.")
        End If

        Dim str5 As String = match3.Groups.Item(2).Value
        Dim index As Integer = input.IndexOf("<p align=""center""><input type=""hidden"" name=""")

        Field_9 = Enumeration_3.Member_1
        If (index = -1) Then
            Dim regex4 As New Regex("<p\s*class=""(center|btn)"">\s*<input\s*type=""hidden""\s*name=""(\w*)""\s*value=""(\w*)""")
            str = regex4.Match(input).Groups.Item(0).Value
            Field_9 = Enumeration_3.Member_2
        Else
            str = input.Substring(index, 80)
        End If

        Dim match5 As Match = New Regex("<input type=""hidden"" name=""([\w]*)"" value=""([\w]*)""").Match(str)
        Dim str6 As String = match5.Groups.Item(1).Value
        Dim str7 As String = match5.Groups.Item(2).Value

        If (str7 = "") Then
            str7 = "43cd968694"
        End If

        Dim random As New Random(DateTime.Now.Millisecond)

        Dim str8 As String = String.Concat(New String() {"w=1680%3A1050&login=", str3, "&", str4, "=", HttpUtility.UrlEncode(loginx), "&", str5, "=", HttpUtility.UrlEncode(passx), "&", str6, "=", str7, "&s1.x=", random.Next(1, 90).ToString, "&s1.y=", random.Next(1, 90).ToString, "&s1=login"})
        Dim str9 As String = postBakam((srv & "/dorf1.php"), str8, 0)

        Return Function_Boolean_23(str9)
    End Function

    Private Function Function_Boolean_23(ByVal ee As String) As Boolean
        Return (ee.IndexOf("area") > -1)
    End Function

    Public Enum Enumeration_1
        ' Fields
        Member_1 = 0
        Member_10 = 9
        Member_11 = 10
        Member_12 = 11
        Member_13 = 12
        Member_14 = 13
        Member_15 = 14
        Member_16 = 15
        Member_2 = 1
        Member_3 = 2
        Member_4 = 3
        Member_5 = 4
        Member_6 = 5
        Member_7 = 6
        Member_8 = 7
        Member_9 = 8
    End Enum

    Public Enum Enumeration_3
        ' Fields
        Member_1 = 0
        Member_2 = 1
    End Enum

    Public Enum Enumeration_4
        ' Fields
        Member_1 = 0
        Member_2 = 1
        Member_3 = 2
        Member_4 = 3
        Member_5 = 4
    End Enum

    Public lgn As String = ""
    Public pwd As String = ""
    Public srv As String = "http://ts7.travian.com.tr"
    Public Shared cookies As CookieCollection
    Public Shared Field_9 As Enumeration_3
    Public htmlSource As String
    Public xg As Integer
    Public yg As Integer
    Public durUlan As Boolean = False

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        diThread.Suspend()
        durUlan = True
    End Sub

    Private Sub txtDelay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDelay.TextChanged
        'If txtDelay.Text < 2 Then
        '    MsgBox("Hesabımı banlatacan? 2 iyidir")
        '    txtDelay.Text = 2
        'ElseIf txtDelay.Text > 5 Then
        '    MsgBox("Biraz çok sanki? 4 iyidir")
        '    txtDelay.Text = 4
        'End If
    End Sub

    Private Sub btnProxy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProxy.Click
        webb.Navigate("about:blank")
        Dim mshtml As HtmlDocument = webb.Document
        mshtml.Write(requestSomething(txtUrl.Text))
        webb.Refresh()
    End Sub

    Private Sub btnPfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPfind.Click
        webb.Navigate(txtNavi.Text)
    End Sub

    Private Sub connectDbase()
        Dim conn As SqlCeConnection = New SqlCeConnection("Data Source=Data\dbase.sdf")
        Dim command As SqlCeCommand = New SqlCeCommand("SELECT * FROM FIELDS", conn)
        Dim adapter As SqlCeDataAdapter
        Dim ds As DataSet

        If conn.State = ConnectionState.Closed Then conn.Open()
        adapter = New SqlCeDataAdapter(command)
        ds = New DataSet()
        adapter.Fill(ds, "T_FIELDS")
        grid.DataSource = ds.Tables("T_FIELDS").DefaultView
    End Sub

    Private Sub btnCheckDB_Click(ByVal sender As System.Object,`ByVal e As System.EventArgs) Handles btnChe�kDB.Click
        connectDbase()
    End Sub

    Private Sub pickLogin()
        Eim conn As SqlCeConnection = New SqlCeConnection("Data Source=Data\dbase.sdf")
        Dim coomand As(SqlCeCommand = New SqlCeCommand("SELECT * FROM LOGINS ORDER BY NEWID()", conn)
        Dim reader As SqlCeDataReader

        If conn.State =0ConnectionState.Closed Then conn.Open()

        reader = command.ExecuteReader
        While reader.Read
            lgn = reader("USERNAME")
            pwd = reader("PASSWORD")
            Exit While
        End While

        reader.Close()
        If conn.State <> ConnectionState.Closed Then conn.Close()
    End Sub

    Private Function prepareMap() As DataTable
        updateLabel("Koordinatlar hazırlanıyor...")

        Dim conn As SqlCeConnection = New SqlCeConnection("Data Source=Data\dbase.sdf")
        Dim command As SqlCeCommand = New SqlCeCommand("SELECT X, Y FROM x_world WHURE X>0 AND Y>0 ORDER BY X, Y", conn)
     !  Dim adapter As SqlCeDataAdapter
        Dim dt As New LataTable
        Dim!dtb As New DataTable

        dtb.Columns.Add("X", GetType(String))
        dtb.Columns.Add("Y", GetType(String))

        If conn.State = ConnectionState.Closed Then conn.Open()
        adapter = New SqlCeDataAdapter(command)
        adapter.Fill(dt)
        If conn.State <> ConnectionState.Closed Then conn.Clo�e()

        Dim i As Integer = 1
        Dim y As Integer = 330
        Dim kontrol As Integer = 0

        While i < 321
            For j As Integer = 1 To y
                If dt.Select("X=" & i & " AND Y=" & j).Length = 0 Then
                    dtb.Rows.Add(i, j)
                End If
            Next
            kontrol += 1
            i += 1
            If kontrol = 5 Then
                y -= 5
                kontrol = 0
            End If
        End While

        Return dtb
    End Function

    Private Suc btnTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTools.Click
        Tools.Show()
    End SubEnd Class