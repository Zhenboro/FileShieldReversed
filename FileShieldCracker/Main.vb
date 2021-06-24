Imports System.Security.Cryptography
Imports System.Text
Public Class Main
    Dim LlaveCriptografica As String = "5jBdlQ1DUMcTJjE9Vx2zfiADvw1xNtc2ZkM"
    Dim tripleDESCryptoServiceProvider_0 As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
    Dim md5CryptoServiceProvider_0 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()

    Dim Root As String = Application.StartupPath
    Dim RutaBase As String = Root & "\FileShield"

    Dim UserName As String
    Dim Password As String
    Dim CryptoKey As String
    Dim Version As String
    Dim Origin As String
    Dim OnlyMe As String
    Dim MaxTrys As String
    Dim DeleteAtMaxTrys As String
    Dim StartLimit As String
    Dim StartLimitCounter As String
    Dim LockAtStartLimit As String
    Dim AutoLockStatus As String
    Dim AutoLockType As String
    Dim AutoLockTypeONE As String
    Dim AutoLockTypeONE_Hour As String
    Dim AutoLockTypeONE_Minute As String
    Dim AutoLockTypeONE_Second As String
    Dim AutoLockTypeTWO As String
    Dim AutoLockTypeTWO_Second As String
    Dim AccessKey As String
    Dim ThemeMode As String
    Dim SaveLogs As String
    Dim ShareMode As String

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'asi podremos usar otra llave si se llegase a cambiar en el programa
        LlaveCriptografica = InputBox("Ingrese la Llave Criptografica Universal de FileShield", "Llave Criptografica", LlaveCriptografica)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadDB()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Modifica la DB para aceptar la "Llave de Acceso" para hacer inicio de sesion y descifrar los archivos.
        ' Enabled/Jump/yrukzLgDUyugdvRerr25OA5dBoG357
        '   Enabled = creo que es para decir que la funcion esta permitida
        '   Jump = creo es el tipo de llave de acceso que esta permitida
        '       Jump = Saltar inicio de sesion y descifrar los archivos
        '       Start = Para iniciar Fileshield
        '   yrukzLgDUyugdvRerr25OA5dBoG357 = identificador???????????? no jaja, es la llave criptografica.
        'le damos a la variable AccessKey el valor de que esta activa la funcion con llave de acceso
        'Q3/EvYI4MRMSbzMLtHVgLiamg5fdbBijP27eiXxuFUrbXiPZxPMoniSyFxQFZ2Gc
        AccessKey = "Enabled/Jump/yrukzLgDUyugdvRerr25OA5dBoG357"
        TextBox15.Text = "Enabled/Jump/yrukzLgDUyugdvRerr25OA5dBoG357"
        SaveDB() 'Guardamos la opcion
        Process.Start("WorFileShield.exe", "Q3/EvYI4MRMSbzMLtHVgLiamg5fdbBijP27eiXxuFUrbXiPZxPMoniSyFxQFZ2Gc") 'iniciamos Fileshield con el argumento AccessKey
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        RichTextBox1.AppendText(vbCrLf & Descifrar(RichTextBox2.Text))
    End Sub

    Dim AutoLockTypeVAR As String
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox13.Text = "True" Then
            AutoLockTypeVAR = "ONE"
            TextBox14.Text = "False"
        ElseIf TextBox14.Text = "True" Then
            AutoLockTypeVAR = "TWO"
            TextBox13.Text = "False"
        End If
        SaveDB()
    End Sub

    Sub LoadDB()
        Dim RAWContent As String = My.Computer.FileSystem.ReadAllText(RutaBase & "\FS_DB.ini")
        Dim DecryptContent As String = Descifrar(RAWContent)
        Dim lines As String() = New TextBox() With {
            .Text = DecryptContent
        }.Lines
        RichTextBox1.AppendText(vbCrLf & "<--- DB Raw Content --->")
        RichTextBox1.AppendText(vbCrLf & RAWContent)
        RichTextBox1.AppendText(vbCrLf & "<--- DB Decrypted Content Loaded --->")
        RichTextBox1.AppendText(vbCrLf & DecryptContent)
        'Sabiendo que:
        '   ">" es el que corta las cadenas para obtener lo que esta luego de esta
        '   Se lee linea por linea, cada linea corresponde a un dato que es leido
        UserName = lines(1).Split(New Char() {">"c})(1).Trim()
        Password = lines(2).Split(New Char() {">"c})(1).Trim()
        CryptoKey = lines(3).Split(New Char() {">"c})(1).Trim()
        Version = lines(4).Split(New Char() {">"c})(1).Trim()
        Origin = lines(5).Split(New Char() {">"c})(1).Trim()
        OnlyMe = lines(6).Split(New Char() {">"c})(1).Trim()
        MaxTrys = lines(7).Split(New Char() {">"c})(1).Trim()
        DeleteAtMaxTrys = lines(8).Split(New Char() {">"c})(1).Trim()
        StartLimit = lines(9).Split(New Char() {">"c})(1).Trim()
        StartLimitCounter = lines(10).Split(New Char() {">"c})(1).Trim()
        LockAtStartLimit = lines(11).Split(New Char() {">"c})(1).Trim()
        AutoLockStatus = lines(12).Split(New Char() {">"c})(1).Trim()
        AutoLockType = lines(13).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeONE = lines(14).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeONE_Hour = lines(15).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeONE_Minute = lines(16).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeONE_Second = lines(17).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeTWO = lines(18).Split(New Char() {">"c})(1).Trim()
        AutoLockTypeTWO_Second = lines(19).Split(New Char() {">"c})(1).Trim()
        AccessKey = lines(20).Split(New Char() {">"c})(1).Trim()
        ThemeMode = lines(21).Split(New Char() {">"c})(1).Trim()
        SaveLogs = lines(22).Split(New Char() {">"c})(1).Trim()
        ShareMode = lines(23).Split(New Char() {">"c})(1).Trim()
        TextBox1.Text = UserName
        TextBox2.Text = Password
        TextBox3.Text = CryptoKey
        TextBox4.Text = Version
        TextBox5.Text = Origin
        TextBox6.Text = OnlyMe
        TextBox7.Text = MaxTrys
        TextBox8.Text = DeleteAtMaxTrys
        TextBox9.Text = StartLimit
        TextBox10.Text = StartLimitCounter
        TextBox11.Text = LockAtStartLimit
        TextBox12.Text = AutoLockStatus
        TextBox13.Text = AutoLockTypeONE
        TextBox14.Text = AutoLockTypeTWO
        TextBox15.Text = AccessKey
        TextBox16.Text = ThemeMode
        TextBox17.Text = SaveLogs
        TextBox18.Text = ShareMode
        RichTextBox1.ScrollToCaret()
    End Sub

    Sub SaveDB()
        Dim FormatoDB As String = "#Worcome FileShield Login DataBase" &
                                vbCrLf & "UserName>" & TextBox1.Text &
                                vbCrLf & "Password>" & TextBox2.Text &
                                vbCrLf & "CryptoKey>" & TextBox3.Text &
                                vbCrLf & "Version>" & TextBox4.Text &
                                vbCrLf & "Origin>" & TextBox5.Text &
                                vbCrLf & "OnlyMe>" & TextBox6.Text &
                                vbCrLf & "MaxTrys>" & TextBox7.Text &
                                vbCrLf & "DeleteAtMaxTrys>" & TextBox8.Text &
                                vbCrLf & "StartLimit>" & TextBox9.Text &
                                vbCrLf & "StartLimitCounter>" & TextBox10.Text &
                                vbCrLf & "LockAtStartLimit>" & TextBox11.Text &
                                vbCrLf & "AutoLockStatus>" & TextBox12.Text &
                                vbCrLf & "AutoLockType>ONE" & AutoLockTypeVAR &
                                vbCrLf & "AutoLockTypeONE>" & TextBox13.Text &
                                vbCrLf & "AutoLockTypeONE_Hour>1" &
                                vbCrLf & "AutoLockTypeONE_Minute>10" &
                                vbCrLf & "AutoLockTypeONE_Second>25" &
                                vbCrLf & "AutoLockTypeTWO>" & TextBox14.Text &
                                vbCrLf & "AutoLockTypeTWO_Second>180" &
                                vbCrLf & "AccessKey>" & TextBox15.Text &
                                vbCrLf & "ThemeMode>" & TextBox16.Text &
                                vbCrLf & "SaveLogs>" & TextBox17.Text &
                                vbCrLf & "ShareMode>" & TextBox18.Text
        Dim EncryptedContent As String = Cifrar(FormatoDB)
        RichTextBox1.AppendText(vbCrLf & "<--- DB Raw Content --->")
        RichTextBox1.AppendText(vbCrLf & FormatoDB)
        RichTextBox1.AppendText(vbCrLf & "<--- DB Encrypted Content Saved --->")
        RichTextBox1.AppendText(vbCrLf & EncryptedContent)
        My.Computer.FileSystem.WriteAllText(RutaBase & "\FS_DB.ini", EncryptedContent, False)
        RichTextBox1.ScrollToCaret()
    End Sub

    'Sub BaseDeDatos() 'Formato de la Base de Datos (1.7.0.0)
    '    "#Worcome FileShield Login DataBase" &
    '    vbCrLf & "UserName>tmpUser" &
    '    vbCrLf & "Password>15243" &
    '    vbCrLf & "CryptoKey>" &
    '    vbCrLf & "Version>" & My.Application.Info.Version.ToString() &
    '    vbCrLf & "Origin>" & Environment.UserName &
    '    vbCrLf & "OnlyMe>False" &
    '    vbCrLf & "MaxTrys>5" &
    '    vbCrLf & "DeleteAtMaxTrys>False" &
    '    vbCrLf & "StartLimit>10" &
    '    vbCrLf & "StartLimitCounter>0" &
    '    vbCrLf & "LockAtStartLimit>False" &
    '    vbCrLf & "AutoLockStatus>False" &
    '    vbCrLf & "AutoLockType>ONE" &
    '    vbCrLf & "AutoLockTypeONE>True" &
    '    vbCrLf & "AutoLockTypeONE_Hour>0" &
    '    vbCrLf & "AutoLockTypeONE_Minute>10" &
    '    vbCrLf & "AutoLockTypeONE_Second>25" &
    '    vbCrLf & "AutoLockTypeTWO>False" &
    '    vbCrLf & "AutoLockTypeTWO_Second>180" &
    '    vbCrLf & "AccessKey>Disabled/Start/None" &
    '    vbCrLf & "ThemeMode>Default" &
    '    vbCrLf & "SaveLogs>False" &
    '    vbCrLf & "ShareMode>False"
    'End Sub

    Public Function Cifrar(ByVal TextIn As String) As String
        Dim result As String
        If TextIn = "" Then
            result = ""
        Else
            tripleDESCryptoServiceProvider_0.Key = md5CryptoServiceProvider_0.ComputeHash(New UnicodeEncoding().GetBytes(LlaveCriptografica))
            tripleDESCryptoServiceProvider_0.Mode = CipherMode.ECB
            Dim cryptoTransform As ICryptoTransform = tripleDESCryptoServiceProvider_0.CreateEncryptor()
            Dim bytes As Byte() = Encoding.ASCII.GetBytes(TextIn)
            result = Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length))
        End If
        Return result
    End Function
    Public Function Descifrar(ByVal TextIn As String) As String
        Dim result As String
        If TextIn = "" Then
            result = ""
        Else
            tripleDESCryptoServiceProvider_0.Key = md5CryptoServiceProvider_0.ComputeHash(New UnicodeEncoding().GetBytes(LlaveCriptografica))
            tripleDESCryptoServiceProvider_0.Mode = CipherMode.ECB
            Dim cryptoTransform As ICryptoTransform = tripleDESCryptoServiceProvider_0.CreateDecryptor()
            Dim array As Byte() = Convert.FromBase64String(TextIn)
            result = Encoding.ASCII.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length))
        End If
        Return result
    End Function
End Class
'TODO
'   Un algoritmo que descifre los archivos protegidos