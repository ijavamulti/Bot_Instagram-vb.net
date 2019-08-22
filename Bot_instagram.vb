Imports System.ComponentModel
Imports System.Text.RegularExpressions, System.Net, System.Text, System.IO
Public Class Form1
    Dim erorrys As String
    Dim Commentid As String
    Dim am As System.Threading.Thread
    Dim posts As New ListBox
    Dim w As New WebClient
    Dim sa As Boolean = False
    Dim min, nub, like_error, like_done, cm_error, cm_done, cm_lkie_done, cm_lkie_error As Single
    Dim cookies, csrftoken, cookies2, csrftoken2 As String
 'Coded By JavaMulti Telegram : @ijava
    ' Dim idmedia As String
    Public Sub New()
 
        ' This call is required by the designer.
        InitializeComponent()
        Me.Size = New Size("200", "235")
        Control.CheckForIllegalCrossThreadCalls = False
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If sa Then
            Try
                If Button3.Tag = 1 Then
                    am = New Threading.Thread(AddressOf Get_EX)
                    am.IsBackground = True
                    am.Start()
                    Button3.Text = "Stop" : Button3.Tag = 2
                ElseIf Button3.Tag = 2 Then
                    Button3.Enabled = False : exitam() : Button3.Text = "Start" : Button3.Tag = 1
                End If
            Catch ex As Exception
 
            End Try
        Else
            MsgBox("Plz, LOGIN fuckers!?")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Tag = 1 Then
            Me.Size = New Size("390", "235")
            Button2.Tag = 2 : Button2.Text = Button2.Text.Replace("Open", "Close").Trim
        ElseIf Button2.Tag = 2 Then
            Me.Size = New Size("200", "235")
            Button2.Tag = 1 : Button2.Text = Button2.Text.Replace("Close", "Open").Trim
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LinkLabel1.Text = Nothing
        If LOGIN_insta(TextBox1.Text, TextBox2.Text) Then
            LinkLabel1.Text = "DONE LOGIN"
            Button2.Enabled = True
        Else
            LinkLabel1.Text = "FALSE LOGIN"
        End If
    End Sub
 
    Private Sub TextBox2_DoubleClick(sender As Object, e As EventArgs) Handles TextBox2.DoubleClick
        If TextBox2.UseSystemPasswordChar = False Then
            TextBox2.UseSystemPasswordChar = True
        ElseIf TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
        End If
    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Try
            If TextBox5.Text > 0.25 Then
                min = TextBox5.Text
            Else
                TextBox5.Text = 0.25
                min = TextBox5.Text
            End If
        Catch ex As Exception
            TextBox5.Text = 0.25
            min = TextBox5.Text
        End Try
    End Sub
 
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True And ListBox1.Items.Count = 0 Then
            CheckBox2.Checked = False
            MsgBox("There No COOKIES!?")
        End If
    End Sub
 
    Private Function LOGIN_insta(ByVal user, ByVal pass)
 
        Using w
            Dim datainput As New System.Collections.Specialized.NameValueCollection
            datainput.Add("username", user)
            datainput.Add("password", pass)
            Try
                w.Headers.Add("user-agent: Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)")
                w.Headers.Add("KeepAlive: True")
                w.Headers.Add("x-csrftoken", "messing")
                w.Headers.Add("accept-language", "en-US,en;q=0.9,ar-SA;q=0.8,ar;q=0.7")
                w.Headers.Add("content-type", "application/x-www-form-urlencoded")
                w.Headers.Add("x-instagram-ajax", "1")
                w.Headers.Add("x-requested-with: XMLHttpRequest")
                w.UploadValues("https://www.instagram.com/accounts/login/ajax/", "POST", datainput)
                If w.ResponseHeaders("set-cookie").Contains("sessionid") Then
                    cookies = Regex.Match(w.ResponseHeaders("set-cookie"), "csrftoken=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "ds_user_id=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "mid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "rur=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "sessionid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "shbid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "shbts=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "urlgen=(.*?);").Value & Space(1)
                    csrftoken = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
                    sa = True : Return True
                Else
                    Return False
                End If
 
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function
 
    Private Sub CoustomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CoustomToolStripMenuItem.Click
        LinkLabel1.Text = Nothing
        TextBox1.Clear()
        TextBox2.Clear()
        Try
            Dim x = InputBox("Your Cookies")
            cookies = Regex.Match(x, "csrftoken=(.*?);").Value & Space(1) & Regex.Match(x, "ds_user_id=(.*?);").Value & Space(1) & Regex.Match(x, "mid=(.*?);").Value & Space(1) & Regex.Match(x, "rur=(.*?);").Value & Space(1) & Regex.Match(x, "sessionid=(.*?);").Value & Space(1) & Regex.Match(x, "shbid=(.*?);").Value & Space(1) & Regex.Match(x, "shbts=(.*?);").Value & Space(1) & Regex.Match(x, "urlgen=(.*?);").Value & Space(1)
            csrftoken = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
            Try
                Dim w As New System.Net.WebClient
                w.Headers.Add("cookie", cookies)
                'w.Headers.Add("x-csrftoken", csrftoken)
                Dim dataos As String = w.DownloadString("https://www.instagram.com/accounts/privacy_and_security/?__a=1")
                If dataos.Contains("form_data") Then
                    Button2.Enabled = True
                    sa = True
                    Dim infostring As String = w.DownloadString("https://i.instagram.com/api/v1/users/" & Regex.Match(cookies, "sessionid=(\d*)").Groups(1).Value & "/info/")
                    TextBox1.Text = Regex.Match(infostring, """username"": ""(\w+)""").Groups(1).Value
                    LinkLabel1.Text = "DONE LOGIN"
                Else
                    MsgBox("Error Cookies")
                End If
            Catch ex As Exception
                MsgBox("Error Cookies")
            End Try
        Catch ex As Exception
            MsgBox("Error")
        End Try
    End Sub
 
    Private Sub AddCustomCOOKIESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddCustomCOOKIESToolStripMenuItem.Click
        Try
            Dim x = InputBox("Your Cookies")
            Dim cookiesy = Regex.Match(x, "csrftoken=(.*?);").Value & Space(1) & Regex.Match(x, "ds_user_id=(.*?);").Value & Space(1) & Regex.Match(x, "mid=(.*?);").Value & Space(1) & Regex.Match(x, "rur=(.*?);").Value & Space(1) & Regex.Match(x, "sessionid=(.*?);").Value & Space(1) & Regex.Match(x, "shbid=(.*?);").Value & Space(1) & Regex.Match(x, "shbts=(.*?);").Value & Space(1) & Regex.Match(x, "urlgen=(.*?);").Value & Space(1)
            'csrftoken = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
            Try
                Dim w As New System.Net.WebClient
                w.Headers.Add("cookie", cookiesy)
                'w.Headers.Add("x-csrftoken", csrftoken)
                Dim dataos As String = w.DownloadString("https://www.instagram.com/accounts/privacy_and_security/?__a=1")
                If dataos.Contains("form_data") Then
                    Dim infostring As String = w.DownloadString("https://i.instagram.com/api/v1/users/" & Regex.Match(cookiesy, "sessionid=(\d*)").Groups(1).Value & "/info/")
                    ListBox1.Items.Add(cookiesy)
                    MsgBox("DONE LOGIN " & Regex.Match(infostring, """username"": ""(\w+)""").Groups(1).Value)
                Else
                    MsgBox("Error Cookies")
                End If
            Catch ex As Exception
                MsgBox("Error Cookies")
            End Try
        Catch ex As Exception
            MsgBox("Error")
        End Try
    End Sub
 
    Private Sub Get_EX()
 
        Do
            Try
                Dim datay As String
                Using w As New WebClient
                    w.Headers.Add("user-agent: Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)")
                    w.Headers.Add("Cookie", cookies)
                    datay = Regex.Unescape(w.DownloadString("https://www.instagram.com/graphql/query/?query_hash=ecd67af449fb6edab7c69a205413bfa7&variables={%22first%22:50}"))
                    Dim a = Regex.Matches(datay, "{""text"":"".*?""shortcode"":"".*?""")
                    For Each ma As Match In a
                        Dim b = ma.Value.ToString
                        If CheckBox1.Checked = True Then
                            If HasArabicCharacters(b) Then
                                Dim c = Regex.Match(b, """shortcode"":""(.*?)""").Groups(1).Value
                                Dim wbs As New WebClient
                                Dim texto As String = wbs.DownloadString("https://www.instagram.com/p/" & c.ToString)
                                Dim mediaid = Regex.Match(texto, "media.?id=(.*?)""").Groups(1).Value
                                'MsgBox(mediaid.ToString)
                                posts.Items.Add(mediaid.ToString)
                                '           ListBox1.Items.Add(mediaid.ToString)
                                Label4.Text = (posts.Items.Count) : Label4.Refresh()
                            Else
 
                            End If
                        ElseIf Not CheckBox1.Checked = True Then
                            Dim c = Regex.Match(b, """shortcode"":""(.*?)""").Groups(1).Value
                            Dim wbs As New WebClient
                            Dim texto As String = wbs.DownloadString("https://www.instagram.com/p/" & c.ToString)
                            Dim mediaid = Regex.Match(texto, "media.?id=(.*?)""").Groups(1).Value
                            'MsgBox(mediaid.ToString)
                            posts.Items.Add(mediaid.ToString)
                            '      ListBox1.Items.Add(mediaid.ToString)
                            Label4.Text = (posts.Items.Count) : Label4.Refresh()
                        End If
                    Next
                    If do_like(posts.Items(nub)) Then
                        like_done += 1
                    Else
                        like_error += 1
                        MessageBox.Show(erorrys)
                    End If
                    If do_comment(posts.Items(nub)) Then
                        cm_done += 1
                        If CheckBox2.Checked = True Then
 
                            For Each cooke In ListBox1.Items
                                cookies2 = (cooke)
                                csrftoken2 = Regex.Match(cooke, "csrftoken=(.*?);").Groups(1).Value
                                If Like_Comment_ID(Commentid, cookies2) Then
                                    cm_lkie_done += 1
                                Else
                                    cm_lkie_error += 1
                                End If
                            Next
                        End If
                    Else
                        cm_error += 1
                    End If
                End Using
 
            Catch ex As Exception
 
            End Try
            nub += 1
            Label5.Text = "Done like: " & like_done & " Error like: " & like_error & vbNewLine & "Done CM: " & cm_done & " Error CM: " & cm_error
            Label6.Text = "Done like: " & cm_lkie_done & " Error like: " & cm_lkie_error
            Try
                System.Threading.Thread.Sleep(60000 * TextBox5.Text)
            Catch ex As Exception
 
            End Try
        Loop
    End Sub
 
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim ss As System.Threading.Thread
        ss = New Threading.Thread(Sub()
                                      Dim x, y
                                      x = InputBox("Uesrname:")
                                      y = InputBox("Password")
                                      If LOGIN2_insta(x, y) Then
                                          ListBox1.Items.Add(cookies2)
                                      Else
                                          MsgBox("Error Acconut")
                                      End If
                                      ss.Abort()
                                  End Sub)
        ss.Start()
    End Sub
 
    Friend Function HasArabicCharacters(ByVal text As String) As Boolean
        Dim regex As Regex = New Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]")
        Return regex.IsMatch(text)
    End Function
    Friend Function do_like(ByVal id)
        Dim wb As New WebClient
        Dim rsa As String
        Try
            Using wb
                wb.Headers.Add("x-csrftoken", csrftoken)
                wb.Headers.Add("cookie", cookies)
                rsa = wb.UploadString("https://www.instagram.com/web/likes/" & id & "/like/", "POST")
                If rsa.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    erorrys = rsa
                    Return False
                End If
            End Using
        Catch ex As System.Net.WebException
            Return False
        End Try
    End Function
    Friend Function do_comment(ByVal id)
        Dim wbo As New WebClient
        Dim datainput As New System.Collections.Specialized.NameValueCollection
        datainput.Add("comment_text", TextBox3.Text)
        datainput.Add("replied_to_comment_id", "")
        Using wbo
            Try
                wbo.Headers.Add("x-csrftoken", csrftoken)
                wbo.Headers.Add("cookie", cookies)
                Dim rsbo = wbo.UploadValues("https://www.instagram.com/web/comments/" & id & "/add/ ", "POST", datainput)
                Dim responsebody = (New Text.UTF8Encoding).GetString(rsbo)
                If responsebody.Contains("""status"": ""ok""") Then
                    Commentid = Regex.Match(responsebody, """id"": ""(.*?)""").Groups(1).Value
                    Return True
                Else
                    Return False
                End If
            Catch ex As System.Net.WebException
                Return False
            End Try
        End Using
 
    End Function
 
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Memberships.Close()
        Application.ExitThread()
        Application.Exit()
    End Sub
    Sub exitam()
        Button3.Enabled = True
        am.Abort()
    End Sub
    Friend Function LOGIN2_insta(ByVal user, ByVal pass)
 
        Using w
            w.Headers.Clear()
            Dim datainput As New System.Collections.Specialized.NameValueCollection
            datainput.Add("username", user)
            datainput.Add("password", pass)
            Try
                w.Headers.Add("user-agent: Mozilla/5.0 (Linux; Android 8.0.0; SAMSUNG SM-J600G Build/R16NW) AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/7.4 Chrome/59.0.3071.125 Mobile Safari/537.36")
                w.Headers.Add("KeepAlive: True")
                w.Headers.Add("x-csrftoken", "messing")
                w.Headers.Add("accept-language", "en-US,en;q=0.9,ar-SA;q=0.8,ar;q=0.7")
                w.Headers.Add("content-type", "application/x-www-form-urlencoded")
                w.Headers.Add("x-instagram-ajax", "1")
                w.Headers.Add("x-requested-with: XMLHttpRequest")
                w.UploadValues("https://www.instagram.com/accounts/login/ajax/", "POST", datainput)
                If w.ResponseHeaders("set-cookie").Contains("sessionid") Then
                    cookies2 = Regex.Match(w.ResponseHeaders("set-cookie"), "csrftoken=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "ds_user_id=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "mid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "rur=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "sessionid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "shbid=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "shbts=(.*?);").Value & Space(1) & Regex.Match(w.ResponseHeaders("set-cookie"), "urlgen=(.*?);").Value & Space(1)
                    sa = True : Return True
                Else
                    Return False
                End If
                ' hahaha Private api :)))
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function
    Public Function Like_Comment_ID(Comment_ID As String, cookiesy As String) As Boolean
 
        Try
 
            If (cookies.Length = 0) Then
                Return False
            Else
 
                Dim csrftoken As String = Regex.Match(cookiesy, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)
 
                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/comments/like/" & Comment_ID & "/"), HttpWebRequest)
 
                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = ("user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36")
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookiesy)
 
                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()
 
                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)
 
 
                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
 
 
                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If
 
 
        Catch ex As Exception
            Return False
        End Try
 
    End Function
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim datay As String
        Try
            Using w As New Net.WebClient
                w.Proxy = Nothing
                datay = w.DownloadString("https://339r.blogspot.com/p/memberships.html")
                'MsgBox(datay)
                If Not datay.Contains(My.Settings.HWID) Then
                    MsgBox("Error!")
                    Memberships.Close()
                    Application.ExitThread()
                End If
            End Using
        Catch ex As Exception
 
        End Try
    End Sub
End Class