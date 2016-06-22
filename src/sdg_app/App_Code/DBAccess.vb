Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DBAccess
    'ＤＢコネクション
    Protected cn As SqlConnection
    Protected tran As SqlTransaction

#Region "接続"
    '--------------------------------------------------------
    ' ＤＢ接続
    ' Input:-
    ' Output:SqlConnection
    '--------------------------------------------------------
    Public Function ConnectDB() As SqlConnection
        Try
            If cn Is Nothing Then
                cn = New SqlConnection(ConfigurationManager.ConnectionStrings("sdgConnStr").ConnectionString)
                cn.Open()
            End If
            Return cn
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '--------------------------------------------------------
    ' ＤＢ接続を切断
    ' Input:-
    '--------------------------------------------------------
    Public Sub DisConnectDB()
        If Not cn Is Nothing Then
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.Dispose()
        End If
    End Sub
#End Region

#Region "トランザクション"
    '--------------------------------------------------------
    ' トランザクション開始
    ' Input:-
    ' Output:OracleTransaction
    '--------------------------------------------------------
    Public Function BeginTransaction() As SqlTransaction
        Try
            tran = cn.BeginTransaction
            Return tran
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '--------------------------------------------------------
    ' トランザクションコミット
    ' Input:-
    '--------------------------------------------------------
    Public Sub Commit()
        Try
            tran.Commit()
            tran.Dispose()
            tran = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '--------------------------------------------------------
    ' トランザクションロールバック
    ' Input:-
    '--------------------------------------------------------
    Public Sub Rollback()
        Try
            If Not tran Is Nothing Then
                tran.Rollback()
                tran.Dispose()
                tran = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region





    ''--------------------------------------------------------
    '' 担当者毎の施設一覧を取得
    '' Input :担当者ID
    '' Output:施設情報
    ''--------------------------------------------------------
    'Public Function GetShisetsuList(ByVal strTantoID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT s.shisetsu_id, s.shisetsu_nm"
    '        sql += " FROM SHISETSU AS s INNER JOIN SANSHOSHISETSU AS ss ON s.shisetsu_id = ss.shisetsu_id"
    '        sql += " WHERE ss.tanto_id = @tanto_id"
    '        sql += " ORDER BY s.shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@tanto_id", strTantoID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 各種区分マスタ一覧を取得
    '' Input :-
    '' Output:各種区分マスタ一覧データテーブル
    ''--------------------------------------------------------
    'Public Function GetKakushuKbnList(ByVal strKbnShurui As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT k.kbn_id, k.kbn_nm"
    '        sql += " FROM KUBUN AS k"
    '        sql += " WHERE k.kbn_shurui = @kbn_shurui"
    '        sql += " ORDER BY k.kbn_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@kbn_shurui", strKbnShurui))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 渡された施設IDで利用可能な利用者区分の一覧を取得
    '' Input :施設ID
    '' Output:利用者区分の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoshaKbnList(ByVal strShisetsuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT k.kbn_id, k.kbn_nm"
    '        sql += " FROM KUBUN AS k"
    '        sql += " INNER JOIN SHISETSUTANKA AS s ON k.kbn_id = s.riyosha_kbn AND k.kbn_shurui = '003'"
    '        sql += " WHERE s.shisetsu_id = @shisetsu_id"
    '        sql += " ORDER BY k.kbn_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 食事箋出力用データを取得
    ' ''' </summary>
    ' ''' <param name="RiyoshaID">利用者IDArrayList</param>
    ' ''' <returns>利用者情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetShokujisenData(ByVal RiyoshaID As ArrayList) As DataTable
    '    Dim sql As String
    '    Dim sqlWhere As String = ""
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        For i As Integer = 0 To RiyoshaID.Count - 1
    '            If sqlWhere <> "" Then
    '                sqlWhere &= " OR "
    '            End If
    '            sqlWhere &= "r.riyosha_id = @riyosha_id" & i.ToString
    '        Next i
    '        sql = "SELECT r.*, s.shisetsu_nm, h.hinmoku_nm"
    '        sql += " FROM RIYOSHA AS r"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " LEFT OUTER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id"
    '        sql += " WHERE " & sqlWhere
    '        sql += " ORDER BY r.shisetsu_id, r.riyosha_kbn, r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            For i As Integer = 0 To RiyoshaID.Count - 1
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_id" & i.ToString, RiyoshaID(i).ToString))
    '            Next i

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用者変更履歴テーブルから最新の履歴と１つ前の履歴を取得
    ' ''' </summary>
    ' ''' <param name="RiyoshaID">利用者ID</param>
    ' ''' <returns>利用者変更履歴情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoshaHenkoRireki(ByVal RiyoshaID As String) As DataTable
    '    Dim sql As String
    '    Dim sqlWhere As String = ""
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT *"
    '        sql += " FROM RIYOSHAHENKO AS r"
    '        sql += " WHERE r.riyosha_id = @riyosha_id1"
    '        sql += "   AND r.henko_renban = ("
    '        sql += "                SELECT MAX(henko_renban)"
    '        sql += "                  FROM RIYOSHAHENKO"
    '        sql += "                 WHERE riyosha_id = r.riyosha_id"
    '        sql += "       )"
    '        sql += " UNION "
    '        sql += " SELECT *"
    '        sql += " FROM RIYOSHAHENKO AS r"
    '        sql += " WHERE r.riyosha_id = @riyosha_id2"
    '        sql += "   AND r.henko_renban = ("
    '        sql += "                SELECT MAX(henko_renban)"
    '        sql += "                  FROM RIYOSHAHENKO AS r2"
    '        sql += "                 WHERE r2.riyosha_id = r.riyosha_id"
    '        sql += "                   AND r2.henko_renban < ("
    '        sql += "                                    SELECT MAX(henko_renban)"
    '        sql += "                                      FROM RIYOSHAHENKO"
    '        sql += "                                     WHERE riyosha_id = r.riyosha_id"
    '        sql += "                       )"
    '        sql += "       )"
    '        sql += " ORDER BY henko_renban DESC"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id1", RiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id2", RiyoshaID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用者情報一覧画面に表示する利用者情報を取得
    ' ''' </summary>
    ' ''' <param name="strTantoID">ログイン担当者ID</param>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <returns>利用者情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoshaList(ByVal strTantoID As String, ByVal strUpdDT As String, Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoshaKbn As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT s.shisetsu_id, s.shisetsu_nm, r.riyosha_id, r.riyosha_nm, k.kbn_nm, DATEADD(hour, 9, r.upd_dt) AS upd_dt"
    '        sql += " FROM SHISETSU AS s"
    '        sql += " INNER JOIN SANSHOSHISETSU AS ss ON s.shisetsu_id = ss.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS r ON s.shisetsu_id = r.shisetsu_id"
    '        sql += " INNER JOIN KUBUN AS k ON r.riyosha_kbn = k.kbn_id AND k.kbn_shurui = '003'"
    '        sql += " WHERE ss.tanto_id = @tanto_id"
    '        If strShisetsuID <> "" Then
    '            sql += "   AND r.shisetsu_id = @shisetsu_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND r.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strUpdDT = "0" Then
    '            sql += " ORDER BY s.shisetsu_id, r.riyosha_id"
    '        ElseIf strUpdDT = "1" Then
    '            sql += " ORDER BY r.upd_dt DESC, s.shisetsu_id, r.riyosha_id"
    '        End If


    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@tanto_id", strTantoID))
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの利用者情報を取得
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>利用者情報データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyosha(ByVal strRiyoshaID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.*, s.haishoku_shurui"
    '        sql += " FROM RIYOSHA AS r"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " WHERE riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用者の新規登録時に使用する利用者IDを返す
    ' ''' </summary>
    ' ''' <returns>利用者IDの最大値＋１</returns>
    ' ''' <remarks></remarks>
    'Public Function GetNewRiyoshaID() As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT RIGHT('000000' + CONVERT(VARCHAR, CONVERT(NUMERIC, IsNull(MAX(riyosha_id), '0')) + 1), 6)"
    '        sql += " FROM RIYOSHA"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'SQL実行
    '            Return cmd.ExecuteScalar

    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 利用テーブルより未請求の利用の件数を取得
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>利用件数</returns>
    ' ''' <remarks></remarks>
    'Public Function GetCountRiyo(ByVal strRiyoshaID As String) As Integer
    '    Dim sql As String

    '    Try
    '        sql = "SELECT COUNT(*)"
    '        sql += " FROM RIYO"
    '        sql += " WHERE riyosha_id = @riyosha_id"
    '        sql += "   AND riyo_kbn = '1'"
    '        sql += "   AND riyo_ym >= (SELECT genzai_ym FROM SEIKYUSHIME WHERE id = '1')"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))

    '            'SQL実行
    '            Return cmd.ExecuteScalar
    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 渡された施設IDの利用データの件数を取得
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <returns>利用件数</returns>
    ' ''' <remarks></remarks>
    'Public Function GetCountShisetsuRiyo(ByVal strShisetsuID As String) As Integer
    '    Dim sql As String

    '    Try
    '        sql = "SELECT COUNT(*)"
    '        sql += " FROM RIYO"
    '        sql += " WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            'SQL実行
    '            Return cmd.ExecuteScalar
    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ''--------------------------------------------------------
    '' 品目一覧を取得
    '' Input :-
    '' Output:品目情報
    ''--------------------------------------------------------
    'Public Function GetHinmokuList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT hi.hinmoku_id, " & _
    '                    " hi.hinmoku_nm, " & _
    '                    " ha.hacchusaki_nm," & _
    '                    " hi.hacchusaki_id," & _
    '                    " hi.shokushu_id," & _
    '                    " hi.option_kbn" & _
    '                    " FROM HINMOKU AS hi " & _
    '                    " INNER JOIN HACCHUSAKI AS ha " & _
    '                    " ON hi.hacchusaki_id = ha.hacchusaki_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 品目一覧（メインのみ）を取得
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <returns>品目データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetHinmokuListMain(ByVal strShisetsuID As String, ByVal strRiyoshaKbn As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT h.hinmoku_id, h.hinmoku_nm"
    '        sql += " FROM HINMOKU AS h"
    '        sql += " INNER JOIN SHISETSUTANKA AS s ON h.hinmoku_id = s.hinmoku_id"
    '        sql += " WHERE s.shisetsu_id = @shisetsu_id"
    '        sql += "   AND s.riyosha_kbn = @riyosha_kbn"
    '        sql += "   AND h.option_kbn = '0'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 発注先情報を取得
    ' ''' </summary>
    ' ''' <param name="strHacchusakiID">発注先ID</param>
    ' ''' <returns>発注先データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetHacchuusakiList(Optional ByVal strHacchusakiID As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT hacchusaki_id, hacchusaki_nm"
    '        sql += " FROM HACCHUSAKI"
    '        If strHacchusakiID <> "" Then
    '            sql += " WHERE hacchusaki_id = @hacchusaki_id"
    '        End If
    '        sql += " ORDER BY hacchusaki_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            If strHacchusakiID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@hacchusaki_id", strHacchusakiID))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 品目情報を取得
    '' Input :品目ID
    '' Output:品目情報
    ''--------------------------------------------------------
    'Public Function GetHinmoku(ByVal strHinmokuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM HINMOKU WHERE hinmoku_id = @hinmoku_id "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@hinmoku_id", strHinmokuID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 区分種類一覧を取得
    '' Input :-
    '' Output:区分種類一覧データテーブル
    ''--------------------------------------------------------
    'Public Function GetKbnShuruiList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT k.kbn_shurui, k.kbn_shurui_nm FROM KUBUN AS k ORDER BY k.kbn_shurui"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 区分情報を取得
    '' Input :区分種類、区分ID
    '' Output:区分情報
    ''--------------------------------------------------------
    'Public Function GetKbn(ByVal strKbnShuruiID As String, ByVal strKbnID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM KUBUN WHERE kbn_shurui = @kbn_shurui AND kbn_id = @kbn_id "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@kbn_id", strKbnID))
    '            cmd.Parameters.Add(New SqlParameter("@kbn_shurui", strKbnShuruiID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 担当者一覧情報を取得
    '' Input :-
    '' Output:担当者一覧
    ''--------------------------------------------------------
    'Public Function GetTantoList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT tanto_id, tanto_nm FROM TANTO "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 担当者情報を取得
    '' Input :担当者ID
    '' Output:担当者情報
    ''--------------------------------------------------------
    'Public Function GetTanto(ByVal strGTantoID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM TANTO WHERE tanto_id = @Gtanto_id "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGTantoID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 担当者情報を取得
    '' Input :権限ID
    '' Output:担当者情報
    ''--------------------------------------------------------
    'Public Function GetKTanto(ByVal strKengenID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM TANTO WHERE kengen_id = @kengen_id "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' パスワードの判定用担当者情報取得
    '' Input :担当者ID、現在のパスワード
    '' Output:担当者情報
    ''--------------------------------------------------------
    'Public Function CheckPassword(ByVal strGTantoID As String, ByVal strPassword As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM TANTO WHERE tanto_id = @tantoID AND password = @password "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@tantoID", strGTantoID))
    '            cmd.Parameters.Add(New SqlParameter("@password", strPassword))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 施設一覧を取得
    '' Input :担当者ID
    '' Output:施設情報
    ''--------------------------------------------------------
    'Public Function GetAllShisetsuList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT shisetsu_id, shisetsu_nm FROM SHISETSU "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' ユーザー権限一覧を取得
    '' Input :-
    '' Output:ユーザー権限一覧
    ''--------------------------------------------------------
    'Public Function GetUKengenList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT kengen_id, kengen_nm FROM KENGEN "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' ユーザー権限情報を取得
    '' Input :-
    '' Output:ユーザー権限一覧
    ''--------------------------------------------------------
    'Public Function GetUKengen(ByVal strKengenID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM KENGEN WHERE kengen_id = @kengen_id "

    '        Using cmd As New SqlCommand(sql, cn)
    '            'SQL文の設定
    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 画面一覧を取得
    '' Input :-
    '' Output:画面一覧
    ''--------------------------------------------------------
    'Public Function GetGamenList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT gamen_id, menu_nm, gamen_nm FROM GAMEN "

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch ex As Exception
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 参照施設情報を取得
    '' Input :施設ID
    '' Output:施設情報
    ''--------------------------------------------------------
    'Public Function GetSanshoShisetsu(ByVal strTantoID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT s.shisetsu_nm, ss.shisetsu_id "
    '        sql += "FROM SANSHOSHISETSU as ss "
    '        sql += "INNER JOIN SHISETSU AS s ON ss.shisetsu_id = s.shisetsu_id "
    '        sql += "WHERE ss.tanto_id = @tanto_id "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@tanto_id", strTantoID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 渡された施設IDで施設情報を取得
    '' Input :施設ID
    '' Output:施設情報
    ''--------------------------------------------------------
    'Public Function GetShisetsu(ByVal strShisetsuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM SHISETSU WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 渡された施設IDで施設単価情報を取得
    '' Input :施設ID
    '' Output:施設単価情報
    ''--------------------------------------------------------
    'Public Function GetShisetsuTanka(ByVal strShisetsuID As String, ByVal strTankaKbn As String, ByVal strRiyoshaKbn As String, ByVal strHinmokuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT K.kbn_nm, K.kbn_id, "
    '        sql += " H.hinmoku_nm, H.hinmoku_id,"
    '        If strTankaKbn = "1" Then
    '            sql += " ST.tanka_zeinuki_kin1 AS tanka_kin1,"
    '            sql += " ST.tanka_zeinuki_kin2 AS tanka_kin2,"
    '            sql += " ST.tanka_zeinuki_kin3 AS tanka_kin3,"
    '            sql += " ST.tanka_zeinuki_kin4 AS tanka_kin4"
    '        ElseIf strTankaKbn = "2" Then
    '            sql += " ST.tanka_zeikomi_kin1 AS tanka_kin1,"
    '            sql += " ST.tanka_zeikomi_kin2 AS tanka_kin2,"
    '            sql += " ST.tanka_zeikomi_kin3 AS tanka_kin3,"
    '            sql += " ST.tanka_zeikomi_kin4 AS tanka_kin4"
    '        Else
    '            sql += " ST.tanka_zeinuki_kin1,"
    '            sql += " ST.tanka_zeinuki_kin2,"
    '            sql += " ST.tanka_zeinuki_kin3,"
    '            sql += " ST.tanka_zeinuki_kin4,"
    '            sql += " ST.tanka_zeikomi_kin1,"
    '            sql += " ST.tanka_zeikomi_kin2,"
    '            sql += " ST.tanka_zeikomi_kin3,"
    '            sql += " ST.tanka_zeikomi_kin4"
    '        End If
    '        sql += " FROM SHISETSUTANKA AS ST"
    '        sql += " INNER JOIN KUBUN AS K ON K.kbn_id = ST.riyosha_kbn AND K.kbn_shurui = '003'"
    '        sql += " INNER JOIN HINMOKU AS H ON H.hinmoku_id = ST.hinmoku_id"
    '        sql += " WHERE shisetsu_id = @shisetsu_id "
    '        If strRiyoshaKbn <> "" Then
    '            sql += " AND ST.riyosha_kbn = @riyosha_kbn "
    '        End If
    '        If strHinmokuID <> "" Then
    '            sql += " AND ST.hinmoku_id = @hinmoku_id "
    '        End If


    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strHinmokuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@hinmoku_id", strHinmokuID))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch ex As Exception
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用者情報を取得
    '' Input :施設ID
    '' Output:利用者情報
    ''--------------------------------------------------------
    'Public Function GetSRiyosha(ByVal strShisetsuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT * FROM RIYOSHA WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch ex As Exception
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 施設単価情報を取得
    '' Input :施設ID、利用者区分
    '' Output:施設単価情報
    ''--------------------------------------------------------
    'Public Function GetShisetsuTankaList(ByVal strShisetsuID As String, ByVal strZei As String, Optional ByVal strhacchusaki As String = "", Optional ByVal strRiyoshaKbn As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT st.shisetsu_id, st.riyosha_kbn, st.hinmoku_id,hi.hinmoku_nm,"
    '        If strZei = "1" Then
    '            sql += " st.tanka_zeinuki_kin1 AS tanka_kin1 , st.tanka_zeinuki_kin2 AS tanka_kin2,"
    '            sql += " st.tanka_zeinuki_kin3 AS tanka_kin3 , st.tanka_zeinuki_kin4 AS tanka_kin4,"
    '        ElseIf strZei = "2" Then
    '            sql += " st.tanka_zeikomi_kin1 AS tanka_kin1, st.tanka_zeikomi_kin2 AS tanka_kin2,"
    '            sql += " st.tanka_zeikomi_kin3 AS tanka_kin3, st.tanka_zeikomi_kin4 AS tanka_kin4,"
    '        End If
    '        sql += " k.kbn_nm AS riyosha_kbn_nm, k2.kbn_nm AS riyo_nyuryoku_kbn_nm, ha.hacchusaki_nm "
    '        sql += " FROM SHISETSUTANKA AS st"
    '        sql += " INNER JOIN HINMOKU AS hi ON hi.hinmoku_id = st.hinmoku_id "
    '        sql += " INNER JOIN SHISETSU AS s ON s.shisetsu_id = st.shisetsu_id"
    '        sql += " INNER JOIN HACCHUSAKI AS ha ON hi.hacchusaki_id = ha.hacchusaki_id"
    '        sql += " INNER JOIN KUBUN AS k ON st.riyosha_kbn = k.kbn_id AND k.kbn_shurui = '003' "
    '        sql += " INNER JOIN KUBUN AS k2 ON s.riyo_nyuryoku_kbn = k2.kbn_id AND k2.kbn_shurui = '004' "
    '        sql += " WHERE st.shisetsu_id = @shisetsu_id"
    '        If strhacchusaki <> "" Then
    '            sql += "   AND hi.hacchusaki_id = @hacchusaki_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND st.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        sql += " ORDER BY hi.hacchusaki_id, st.riyosha_kbn, hi.hinmoku_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            If strhacchusaki <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@hacchusaki_id", strhacchusaki))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch ex As Exception
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 施設単価一覧を表示するための施設情報を取得
    '' Input :施設ID、利用者区分
    '' Output:施設単価情報
    ''--------------------------------------------------------
    'Public Function GetShisetsuInfo(ByVal strShisetsuID As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT haishoku_shurui, seikyu_tanka_kbn"
    '        sql += " FROM SHISETSU"
    '        sql += " WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))


    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch ex As Exception
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用者テーブル登録
    ' ''' </summary>
    ' ''' <param name="ht">ハッシュテーブル</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function InsertRiyosha(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者テーブルの登録処理 ------------------------------------------------
    '        sql = "INSERT INTO RIYOSHA ("
    '        sql += " shisetsu_id, jotai_kbn, riyosha_id, riyosha_kbn, riyosha_nm, seibetsu, seinen_ymd, busho_nm, shiharai_houhou_id,"
    '        sql += " kaishi_ymd, kaishi_timezone, chushi_ymd, chushi_timezone, kyushi_kaishi_ymd, kyushi_kaishi_timezone, kyushi_shuryo_ymd, kyushi_shuryo_timezone,"
    '        sql += " day_riyo_youbi, kesshoku_youbi, kesshoku_haishoku_shurui, shokushu_id, hinmoku_id, shiji_cal, shiji_enbun,"
    '        sql += " shokuji_keitai_id, shokuji_keitai_etc, shushoku_id, shushokuryo, shushoku_etc, choshoku_main_kbn, choshoku_option_kbn,"
    '        sql += " choshoku_option_etc, shiji_eiyoryo, kinshishoku, etc, shokusatsu1, shokusatsu2, shokusatsu3, shokusatsu4, hakko_ymd, kinyu_user_nm, uketsuke_user_nm,"
    '        sql += " ins_dt, ins_user_id, upd_dt, upd_user_id"
    '        sql += ") VALUES ("
    '        sql += " @shisetsu_id, @jotai_kbn, @riyosha_id, @riyosha_kbn, @riyosha_nm, @seibetsu, @seinen_ymd, @busho_nm, @shiharai_houhou_id,"
    '        sql += " @kaishi_ymd, @kaishi_timezone, @chushi_ymd, @chushi_timezone, @kyushi_kaishi_ymd, @kyushi_kaishi_timezone, @kyushi_shuryo_ymd, @kyushi_shuryo_timezone,"
    '        sql += " @day_riyo_youbi, @kesshoku_youbi, @kesshoku_haishoku_shurui, @shokushu_id, @hinmoku_id, @shiji_cal, @shiji_enbun,"
    '        sql += " @shokuji_keitai_id, @shokuji_keitai_etc, @shushoku_id, @shushokuryo, @shushoku_etc, @choshoku_main_kbn, @choshoku_option_kbn,"
    '        sql += " @choshoku_option_etc, @shiji_eiyoryo, @kinshishoku, @etc, @shokusatsu1, @shokusatsu2, @shokusatsu3, @shokusatsu4, @hakko_ymd, @kinyu_user_nm, @uketsuke_user_nm,"
    '        sql += " @ins_dt, @ins_user_id, @upd_dt, @upd_user_id"
    '        sql += ")"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim insdt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", ht("shisetsu_id")))
    '            cmd.Parameters.Add(New SqlParameter("jotai_kbn", ht("jotai_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", ht("riyosha_id")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_kbn", ht("riyosha_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_nm", ht("riyosha_nm")))
    '            cmd.Parameters.Add(New SqlParameter("seibetsu", ht("seibetsu")))
    '            cmd.Parameters.Add(New SqlParameter("seinen_ymd", ht("seinen_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("busho_nm", ht("busho_nm")))
    '            cmd.Parameters.Add(New SqlParameter("shiharai_houhou_id", ht("shiharai_houhou_id")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_ymd", ht("kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_timezone", ht("kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_ymd", ht("chushi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_timezone", ht("chushi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_ymd", ht("kyushi_kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_timezone", ht("kyushi_kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_ymd", ht("kyushi_shuryo_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_timezone", ht("kyushi_shuryo_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("day_riyo_youbi", ht("day_riyo_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_youbi", ht("kesshoku_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_haishoku_shurui", ht("kesshoku_haishoku_shurui")))
    '            cmd.Parameters.Add(New SqlParameter("shokushu_id", ht("shokushu_id")))
    '            cmd.Parameters.Add(New SqlParameter("hinmoku_id", ht("hinmoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_cal", If(ht("shiji_cal") = "", DBNull.Value, ht("shiji_cal"))))
    '            cmd.Parameters.Add(New SqlParameter("shiji_enbun", If(ht("shiji_enbun") = "", DBNull.Value, ht("shiji_enbun"))))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_id", ht("shokuji_keitai_id")))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_etc", ht("shokuji_keitai_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_id", ht("shushoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shushokuryo", If(ht("shushokuryo") = "", DBNull.Value, ht("shushokuryo"))))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_etc", ht("shushoku_etc")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_main_kbn", ht("choshoku_main_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_kbn", ht("choshoku_option_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_etc", ht("choshoku_option_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_eiyoryo", If(ht("shiji_eiyoryo") = "", DBNull.Value, ht("shiji_eiyoryo"))))
    '            cmd.Parameters.Add(New SqlParameter("kinshishoku", ht("kinshishoku")))
    '            cmd.Parameters.Add(New SqlParameter("etc", ht("etc")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu1", ht("shokusatsu1")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu2", ht("shokusatsu2")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu3", ht("shokusatsu3")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu4", ht("shokusatsu4")))
    '            cmd.Parameters.Add(New SqlParameter("hakko_ymd", ht("hakko_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kinyu_user_nm", ht("kinyu_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("uketsuke_user_nm", ht("uketsuke_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("ins_dt", insdt))
    '            cmd.Parameters.Add(New SqlParameter("ins_user_id", ht("user_id")))
    '            cmd.Parameters.Add(New SqlParameter("upd_dt", insdt))
    '            cmd.Parameters.Add(New SqlParameter("upd_user_id", ht("user_id")))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 利用者テーブル更新
    ' ''' </summary>
    ' ''' <param name="ht">ハッシュテーブル</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function UpdateRiyosha(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者テーブルの更新処理 ------------------------------------------------
    '        sql = "UPDATE RIYOSHA SET"
    '        sql += " shisetsu_id = @shisetsu_id,"
    '        sql += " jotai_kbn = @jotai_kbn,"
    '        sql += " riyosha_kbn = @riyosha_kbn,"
    '        sql += " riyosha_nm = @riyosha_nm,"
    '        sql += " seibetsu = @seibetsu,"
    '        sql += " seinen_ymd = @seinen_ymd,"
    '        sql += " busho_nm = @busho_nm,"
    '        sql += " shiharai_houhou_id = @shiharai_houhou_id,"
    '        sql += " kaishi_ymd = @kaishi_ymd,"
    '        sql += " kaishi_timezone = @kaishi_timezone,"
    '        sql += " chushi_ymd = @chushi_ymd,"
    '        sql += " chushi_timezone = @chushi_timezone,"
    '        sql += " kyushi_kaishi_ymd = @kyushi_kaishi_ymd,"
    '        sql += " kyushi_kaishi_timezone = @kyushi_kaishi_timezone,"
    '        sql += " kyushi_shuryo_ymd = @kyushi_shuryo_ymd,"
    '        sql += " kyushi_shuryo_timezone = @kyushi_shuryo_timezone,"
    '        sql += " day_riyo_youbi = @day_riyo_youbi,"
    '        sql += " kesshoku_youbi = @kesshoku_youbi,"
    '        sql += " kesshoku_haishoku_shurui = @kesshoku_haishoku_shurui,"
    '        sql += " shokushu_id = @shokushu_id,"
    '        sql += " hinmoku_id = @hinmoku_id,"
    '        sql += " shiji_cal = @shiji_cal,"
    '        sql += " shiji_enbun = @shiji_enbun,"
    '        sql += " shokuji_keitai_id = @shokuji_keitai_id,"
    '        sql += " shokuji_keitai_etc = @shokuji_keitai_etc,"
    '        sql += " shushoku_id = @shushoku_id,"
    '        sql += " shushokuryo = @shushokuryo,"
    '        sql += " shushoku_etc = @shushoku_etc,"
    '        sql += " choshoku_main_kbn = @choshoku_main_kbn,"
    '        sql += " choshoku_option_kbn = @choshoku_option_kbn,"
    '        sql += " choshoku_option_etc = @choshoku_option_etc,"
    '        sql += " shiji_eiyoryo = @shiji_eiyoryo,"
    '        sql += " kinshishoku = @kinshishoku,"
    '        sql += " etc = @etc,"
    '        sql += " shokusatsu1 = @shokusatsu1,"
    '        sql += " shokusatsu2 = @shokusatsu2,"
    '        sql += " shokusatsu3 = @shokusatsu3,"
    '        sql += " shokusatsu4 = @shokusatsu4,"
    '        sql += " hakko_ymd = @hakko_ymd,"
    '        sql += " kinyu_user_nm = @kinyu_user_nm,"
    '        sql += " uketsuke_user_nm = @uketsuke_user_nm,"
    '        sql += " upd_dt = @upd_dt,"
    '        sql += " upd_user_id = @upd_user_id"
    '        sql += " WHERE riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim upddt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", ht("shisetsu_id")))
    '            cmd.Parameters.Add(New SqlParameter("jotai_kbn", ht("jotai_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_kbn", ht("riyosha_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_nm", ht("riyosha_nm")))
    '            cmd.Parameters.Add(New SqlParameter("seibetsu", ht("seibetsu")))
    '            cmd.Parameters.Add(New SqlParameter("seinen_ymd", ht("seinen_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("busho_nm", ht("busho_nm")))
    '            cmd.Parameters.Add(New SqlParameter("shiharai_houhou_id", ht("shiharai_houhou_id")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_ymd", ht("kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_timezone", ht("kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_ymd", ht("chushi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_timezone", ht("chushi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_ymd", ht("kyushi_kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_timezone", ht("kyushi_kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_ymd", ht("kyushi_shuryo_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_timezone", ht("kyushi_shuryo_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("day_riyo_youbi", ht("day_riyo_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_youbi", ht("kesshoku_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_haishoku_shurui", ht("kesshoku_haishoku_shurui")))
    '            cmd.Parameters.Add(New SqlParameter("shokushu_id", ht("shokushu_id")))
    '            cmd.Parameters.Add(New SqlParameter("hinmoku_id", ht("hinmoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_cal", If(ht("shiji_cal") = "", DBNull.Value, ht("shiji_cal"))))
    '            cmd.Parameters.Add(New SqlParameter("shiji_enbun", If(ht("shiji_enbun") = "", DBNull.Value, ht("shiji_enbun"))))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_id", ht("shokuji_keitai_id")))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_etc", ht("shokuji_keitai_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_id", ht("shushoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shushokuryo", If(ht("shushokuryo") = "", DBNull.Value, ht("shushokuryo"))))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_etc", ht("shushoku_etc")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_main_kbn", ht("choshoku_main_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_kbn", ht("choshoku_option_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_etc", ht("choshoku_option_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_eiyoryo", If(ht("shiji_eiyoryo") = "", DBNull.Value, ht("shiji_eiyoryo"))))
    '            cmd.Parameters.Add(New SqlParameter("kinshishoku", ht("kinshishoku")))
    '            cmd.Parameters.Add(New SqlParameter("etc", ht("etc")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu1", ht("shokusatsu1")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu2", ht("shokusatsu2")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu3", ht("shokusatsu3")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu4", ht("shokusatsu4")))
    '            cmd.Parameters.Add(New SqlParameter("hakko_ymd", ht("hakko_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kinyu_user_nm", ht("kinyu_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("uketsuke_user_nm", ht("uketsuke_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("upd_dt", upddt))
    '            cmd.Parameters.Add(New SqlParameter("upd_user_id", ht("user_id")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", ht("riyosha_id")))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの利用者情報を削除
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyosha(ByVal strRiyoshaID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE RIYOSHA"
    '        sql += " WHERE riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの利用情報を全て削除する
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyo(ByVal strRiyoshaID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE RIYO"
    '        sql += " WHERE riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された年月日、時間帯、利用者IDの実費負担品でない利用情報を削除する
    ' ''' </summary>
    ' ''' <param name="strRiyoYMD">利用日</param>
    ' ''' <param name="strRiyoTimezone">利用時間帯</param>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyoMain(ByVal strRiyoYMD As String, ByVal strRiyoTimezone As String, ByVal strRiyoshaID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE r"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id"
    '        sql += " WHERE r.riyosha_id = @riyosha_id"
    '        sql += "   AND r.riyo_ym + RIGHT('00' + CONVERT(varchar, r.riyo_d), 2) = @riyo_ymd"
    '        sql += "   AND r.riyo_timezone = @riyo_timezone"
    '        sql += "   AND h.option_kbn = '0'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("riyo_ymd", strRiyoYMD))
    '            cmd.Parameters.Add(New SqlParameter("riyo_timezone", strRiyoTimezone))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 利用者変更履歴テーブル登録
    ' ''' </summary>
    ' ''' <param name="ht">ハッシュテーブル</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function InsertRiyoshaHenko(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者変更履歴テーブルの登録処理 ------------------------------------------------
    '        sql = "INSERT INTO RIYOSHAHENKO ("
    '        sql += " riyosha_id, henko_renban, shisetsu_id, jotai_kbn, riyosha_kbn, riyosha_nm, seibetsu, seinen_ymd, busho_nm, shiharai_houhou_id,"
    '        sql += " kaishi_ymd, kaishi_timezone, chushi_ymd, chushi_timezone, kyushi_kaishi_ymd, kyushi_kaishi_timezone, kyushi_shuryo_ymd, kyushi_shuryo_timezone,"
    '        sql += " day_riyo_youbi, kesshoku_youbi, kesshoku_haishoku_shurui, shokushu_id, hinmoku_id, shiji_cal, shiji_enbun,"
    '        sql += " shokuji_keitai_id, shokuji_keitai_etc, shushoku_id, shushokuryo, shushoku_etc, choshoku_main_kbn, choshoku_option_kbn,"
    '        sql += " choshoku_option_etc, shiji_eiyoryo, kinshishoku, etc, shokusatsu1, shokusatsu2, shokusatsu3, shokusatsu4, hakko_ymd, kinyu_user_nm, uketsuke_user_nm,"
    '        sql += " ins_dt, ins_user_id, upd_dt, upd_user_id"
    '        sql += ") VALUES ("
    '        sql += " @riyosha_id1,"
    '        sql += " (SELECT IsNull(MAX(henko_renban), 0) + 1 FROM RIYOSHAHENKO WHERE riyosha_id = @riyosha_id2),"
    '        sql += " @shisetsu_id, @jotai_kbn, @riyosha_kbn, @riyosha_nm, @seibetsu, @seinen_ymd, @busho_nm, @shiharai_houhou_id,"
    '        sql += " @kaishi_ymd, @kaishi_timezone, @chushi_ymd, @chushi_timezone, @kyushi_kaishi_ymd, @kyushi_kaishi_timezone, @kyushi_shuryo_ymd, @kyushi_shuryo_timezone,"
    '        sql += " @day_riyo_youbi, @kesshoku_youbi, @kesshoku_haishoku_shurui, @shokushu_id, @hinmoku_id, @shiji_cal, @shiji_enbun,"
    '        sql += " @shokuji_keitai_id, @shokuji_keitai_etc, @shushoku_id, @shushokuryo, @shushoku_etc, @choshoku_main_kbn, @choshoku_option_kbn,"
    '        sql += " @choshoku_option_etc, @shiji_eiyoryo, @kinshishoku, @etc, @shokusatsu1, @shokusatsu2, @shokusatsu3, @shokusatsu4, @hakko_ymd, @kinyu_user_nm, @uketsuke_user_nm,"
    '        sql += " @ins_dt, @ins_user_id, @upd_dt, @upd_user_id"
    '        sql += ")"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim insdt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id1", ht("riyosha_id")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id2", ht("riyosha_id")))
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", ht("shisetsu_id")))
    '            cmd.Parameters.Add(New SqlParameter("jotai_kbn", ht("jotai_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_kbn", ht("riyosha_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_nm", ht("riyosha_nm")))
    '            cmd.Parameters.Add(New SqlParameter("seibetsu", ht("seibetsu")))
    '            cmd.Parameters.Add(New SqlParameter("seinen_ymd", ht("seinen_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("busho_nm", ht("busho_nm")))
    '            cmd.Parameters.Add(New SqlParameter("shiharai_houhou_id", ht("shiharai_houhou_id")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_ymd", ht("kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kaishi_timezone", ht("kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_ymd", ht("chushi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("chushi_timezone", ht("chushi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_ymd", ht("kyushi_kaishi_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_kaishi_timezone", ht("kyushi_kaishi_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_ymd", ht("kyushi_shuryo_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kyushi_shuryo_timezone", ht("kyushi_shuryo_timezone")))
    '            cmd.Parameters.Add(New SqlParameter("day_riyo_youbi", ht("day_riyo_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_youbi", ht("kesshoku_youbi")))
    '            cmd.Parameters.Add(New SqlParameter("kesshoku_haishoku_shurui", ht("kesshoku_haishoku_shurui")))
    '            cmd.Parameters.Add(New SqlParameter("shokushu_id", ht("shokushu_id")))
    '            cmd.Parameters.Add(New SqlParameter("hinmoku_id", ht("hinmoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_cal", If(ht("shiji_cal") = "", DBNull.Value, ht("shiji_cal"))))
    '            cmd.Parameters.Add(New SqlParameter("shiji_enbun", If(ht("shiji_enbun") = "", DBNull.Value, ht("shiji_enbun"))))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_id", ht("shokuji_keitai_id")))
    '            cmd.Parameters.Add(New SqlParameter("shokuji_keitai_etc", ht("shokuji_keitai_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_id", ht("shushoku_id")))
    '            cmd.Parameters.Add(New SqlParameter("shushokuryo", If(ht("shushokuryo") = "", DBNull.Value, ht("shushokuryo"))))
    '            cmd.Parameters.Add(New SqlParameter("shushoku_etc", ht("shushoku_etc")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_main_kbn", ht("choshoku_main_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_kbn", ht("choshoku_option_kbn")))
    '            cmd.Parameters.Add(New SqlParameter("choshoku_option_etc", ht("choshoku_option_etc")))
    '            cmd.Parameters.Add(New SqlParameter("shiji_eiyoryo", If(ht("shiji_eiyoryo") = "", DBNull.Value, ht("shiji_eiyoryo"))))
    '            cmd.Parameters.Add(New SqlParameter("kinshishoku", ht("kinshishoku")))
    '            cmd.Parameters.Add(New SqlParameter("etc", ht("etc")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu1", ht("shokusatsu1")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu2", ht("shokusatsu2")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu3", ht("shokusatsu3")))
    '            cmd.Parameters.Add(New SqlParameter("shokusatsu4", ht("shokusatsu4")))
    '            cmd.Parameters.Add(New SqlParameter("hakko_ymd", ht("hakko_ymd")))
    '            cmd.Parameters.Add(New SqlParameter("kinyu_user_nm", ht("kinyu_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("uketsuke_user_nm", ht("uketsuke_user_nm")))
    '            cmd.Parameters.Add(New SqlParameter("ins_dt", insdt))
    '            cmd.Parameters.Add(New SqlParameter("ins_user_id", ht("user_id")))
    '            cmd.Parameters.Add(New SqlParameter("upd_dt", DBNull.Value))
    '            cmd.Parameters.Add(New SqlParameter("upd_user_id", DBNull.Value))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの利用者変更履歴を全て削除する
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyoshaHenko(ByVal strRiyoshaID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '利用者変更履歴テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE RIYOSHAHENKO"
    '        sql += " WHERE riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function
    ''--------------------------------------------------------
    '' 参照施設情報の追加
    ''--------------------------------------------------------
    'Public Function Insert_SanshoShisetsu(ByVal strGTantoID As String, ByVal strShisetsuID As String, ByVal strSTantoID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        'SQL文の設定
    '        sql = "INSERT INTO SANSHOSHISETSU "
    '        sql += "VALUES (@Gtanto_id , "
    '        sql += "@shisetsu_id, "
    '        sql += "@createDate, "
    '        sql += "@Stanto_id, "
    '        sql += "@updateDate, "
    '        sql += "@Stanto_id )"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGTantoID))
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@Stanto_id", strSTantoID))
    '            cmd.Parameters.Add(New SqlParameter("@createDate", Now))
    '            cmd.Parameters.Add(New SqlParameter("@updateDate", Now))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' 参照施設情報の削除
    ''--------------------------------------------------------
    'Public Function Delete_SanshoShisetsu(ByVal strTantoID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        'SQL文の設定
    '        sql = "DELETE FROM SANSHOSHISETSU WHERE tanto_id = @Gtanto_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strTantoID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using
    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' 担当者情報の追加
    ''--------------------------------------------------------
    'Public Function Insert_Tanto(ByVal strGtantoID As String, ByVal strTantoNM As String, ByVal strBushoNM As String, ByVal strPass As String, ByVal strKengenID As String, ByVal strRiyochuKBN As String, ByVal strLockKBN As String, ByVal strStantoID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '追加処理
    '        'SQL文の設定
    '        sql = "INSERT INTO TANTO "
    '        sql += "VALUES (@Gtanto_id , "
    '        sql += "@tanto_nm,"
    '        sql += "@busho_nm, "
    '        sql += "@password, "
    '        sql += "@kengen_id, "
    '        sql += "@riyochu_kbn, "
    '        sql += "@lock_kbn, "
    '        sql += "@createDate, "
    '        sql += "@Stanto_id, "
    '        sql += "@updateDate, "
    '        sql += "@Stanto_id) "

    '        Using cmd As New SqlCommand(sql, cn)

    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGtantoID))
    '            cmd.Parameters.Add(New SqlParameter("@tanto_nm", strTantoNM))
    '            cmd.Parameters.Add(New SqlParameter("@busho_nm", strBushoNM))
    '            cmd.Parameters.Add(New SqlParameter("@password", strPass))
    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))
    '            cmd.Parameters.Add(New SqlParameter("@riyochu_kbn", strRiyochuKBN))
    '            cmd.Parameters.Add(New SqlParameter("@lock_kbn", strLockKBN))
    '            cmd.Parameters.Add(New SqlParameter("@Stanto_id", strStantoID))
    '            cmd.Parameters.Add(New SqlParameter("@updateDate", Now))
    '            cmd.Parameters.Add(New SqlParameter("@createDate", Now))

    '            '担当者マスタの更新
    '            Dim rc As Integer = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw
    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' 担当者情報の更新
    ''--------------------------------------------------------
    'Public Function Update_Tanto(ByVal strGtantoID As String, ByVal strTantoNM As String, ByVal strBushoNM As String, ByVal strKengenID As String, ByVal strRiyochuKBN As String, ByVal strStantoID As String, Optional ByVal strPass As String = "") As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '更新処理
    '        'SQL文の設定
    '        sql = "UPDATE TANTO "
    '        sql += "SET tanto_nm = @tanto_nm ,"
    '        sql += "busho_nm = @busho_nm, "
    '        If strPass <> "" Then
    '            sql += "password = @password, "
    '        End If
    '        sql += "kengen_id = @kengen_id, "
    '        sql += "riyochu_kbn = @riyochu_kbn, "
    '        sql += "upd_dt = @updateDate, "
    '        sql += "upd_user_id = @Stanto_id "
    '        sql += "WHERE tanto_id = @Gtanto_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGtantoID))
    '            cmd.Parameters.Add(New SqlParameter("@tanto_nm", strTantoNM))
    '            cmd.Parameters.Add(New SqlParameter("@busho_nm", strBushoNM))
    '            If strPass <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@password", strPass))
    '            End If
    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))
    '            cmd.Parameters.Add(New SqlParameter("@riyochu_kbn", strRiyochuKBN))
    '            cmd.Parameters.Add(New SqlParameter("@Stanto_id", strStantoID))
    '            cmd.Parameters.Add(New SqlParameter("@updateDate", Now))

    '            '担当者マスタの更新
    '            Dim rc As Integer = cmd.ExecuteNonQuery()

    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    ''--------------------------------------------------------
    '' 担当者情報の削除
    ''--------------------------------------------------------
    'Public Function Delete_Tanto(ByVal strGtantoID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '削除処理
    '        'SQL文の設定
    '        sql = "DELETE FROM TANTO WHERE tanto_id = @Gtanto_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGtantoID))
    '            '担当者マスタの更新
    '            Dim rc As Integer = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True


    '    Catch ex As Exception
    '        Throw
    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' ユーザー権限の追加
    ''--------------------------------------------------------
    'Public Function Insert_UKengen(ByVal strKengenID As String, ByVal strKengenNM As String, ByVal strGamenID As String, ByVal strUseOK As String, ByVal strSTantoID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        'SQL文の設定
    '        sql = "INSERT INTO KENGEN "
    '        sql += "VALUES (@kengen_id , "
    '        sql += "@kengen_nm, "
    '        sql += "@gamen_id, "
    '        sql += "@use_ok, "
    '        sql += "@createDate, "
    '        sql += "@Stanto_id, "
    '        sql += "@updateDate, "
    '        sql += "@Stanto_id )"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))
    '            cmd.Parameters.Add(New SqlParameter("@kengen_nm", strKengenNM))
    '            cmd.Parameters.Add(New SqlParameter("@gamen_id", strGamenID))
    '            cmd.Parameters.Add(New SqlParameter("@use_ok", strUseOK))
    '            cmd.Parameters.Add(New SqlParameter("@Stanto_id", strSTantoID))
    '            cmd.Parameters.Add(New SqlParameter("@createDate", Now))
    '            cmd.Parameters.Add(New SqlParameter("@updateDate", Now))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' ユーザー権限の削除
    ''--------------------------------------------------------
    'Public Function Delete_UKengen(ByVal strKengenID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        'SQL文の設定
    '        sql = "DELETE FROM KENGEN WHERE kengen_id = @kengen_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@kengen_id", strKengenID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using
    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ' ''' <summary>
    ' ''' ユーザー権限の新規登録時に使用するユーザー権限IDを返す
    ' ''' </summary>
    ' ''' <returns>ユーザー権限IDの最大値＋１</returns>
    ' ''' <remarks></remarks>
    'Public Function GetNewKengenID() As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT RIGHT('000' + CONVERT(VARCHAR, CONVERT(NUMERIC, IsNull(MAX(kengen_id), '0')) + 1), 3)"
    '        sql += " FROM KENGEN"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'SQL実行
    '            Return cmd.ExecuteScalar

    '        End Using

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' 施設ID削除に伴う施設単価情報の削除
    ''--------------------------------------------------------
    'Public Function Delete_ShisetsuTanka(ByVal strShisetsuID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        'SQL文の設定
    '        sql = "DELETE FROM SHISETSUTANKA WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using
    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function
    ''--------------------------------------------------------
    '' 施設情報の削除
    ''--------------------------------------------------------
    'Public Function Delete_Shisetsu(ByVal strShisetsuID As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        'SQL文の設定
    '        sql = "DELETE FROM SHISETSU WHERE shisetsu_id = @shisetsu_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using
    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try

    'End Function



    ' ''' <summary>
    ' ''' 渡されたDataReaderの構造を持ったDataTableを作成する
    ' ''' </summary>
    ' ''' <param name="reader">DataReader</param>
    ' ''' <returns>DataTable</returns>
    ' ''' <remarks></remarks>
    'Public Function CreateSchemaDataTable(ByVal reader As SqlDataReader) As DataTable
    '    If reader Is Nothing OrElse reader.IsClosed Then
    '        Return Nothing
    '    End If

    '    Dim schema As DataTable = reader.GetSchemaTable()
    '    Dim dt As DataTable = New DataTable()

    '    For Each row As DataRow In schema.Rows
    '        Dim col As DataColumn = New DataColumn()
    '        col.ColumnName = row("ColumnName").ToString()
    '        col.DataType = Type.GetType(row("DataType").ToString())

    '        If col.DataType.Equals(GetType(String)) Then
    '            col.MaxLength = Integer.Parse(row("ColumnSize"))
    '        End If
    '        dt.Columns.Add(col)
    '    Next

    '    Return dt
    'End Function


    ''--------------------------------------------------------
    '' 渡された施設IDで利用可能な利用年月の一覧を取得
    '' Input :施設ID
    '' Output:利用年月の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoYMList(ByVal strShisetsuID As String) As DataTable
    '    Dim sql As String
    '    Dim nowTime As DateTime
    '    Dim strYM As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable
    '    Dim bFound As Boolean

    '    Try
    '        sql = "SELECT DISTINCT r.riyo_ym,"
    '        sql += " Case when r.riyo_ym  > '19890107' then '平成' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1988) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '19261224' then '昭和' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1925) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '19120729' then '大正' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1911) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '18680124' then '明治' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1867) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " end riyo_ymH"
    '        sql += " FROM RIYO AS r"
    '        If strShisetsuID <> "" Then
    '            sql += " WHERE r.shisetsu_id = @shisetsu_id"
    '        End If
    '        sql += " ORDER BY r.riyo_ym"

    '        Using cmd As New SqlCommand(sql, cn)

    '            If strShisetsuID <> "" Then
    '                'パラメータ追加
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                row(0) = rd.GetValue(0)
    '                row(1) = rd.GetValue(1)
    '                dt.Rows.Add(row)
    '            End While
    '        End Using
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '        Dim japaneseCulture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", False)
    '        '日付の書式に和暦を指定
    '        japaneseCulture.DateTimeFormat.Calendar = New System.Globalization.JapaneseCalendar()
    '        strYM = nowTime.ToString("yyyyMM")
    '        '請求締管理テーブルにあるかチェックする
    '        Dim sdt As New DataTable
    '        sdt = GetSeikyuYM()
    '        If Not sdt Is Nothing AndAlso sdt.Rows.Count > 0 Then
    '            strYM = sdt.Rows(0).Item(1)
    '            nowTime = New DateTime(Mid(strYM, 1, 4), Mid(strYM, 5, 2), "01")
    '            For n = 0 To 1
    '                strYM = nowTime.AddMonths(n).ToString("yyyyMM")
    '                Dim row = dt.NewRow
    '                row(0) = strYM
    '                row(1) = nowTime.AddMonths(n).ToString("ggyy年MM月", japaneseCulture)
    '                If dt.Rows.Count = 0 Then
    '                    dt.Rows.Add(row)
    '                Else
    '                    bFound = False
    '                    For i As Integer = 0 To dt.Rows.Count - 1
    '                        If dt.Rows(i).Item(0) = strYM Then
    '                            bFound = True
    '                            Exit For
    '                        End If
    '                    Next i
    '                    If bFound = False Then
    '                        dt.Rows.Add(row)
    '                    End If
    '                End If
    '            Next n
    '        End If
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 請求締管理テーブルを取得
    '' Input :なし
    '' Output:請求締管理情報
    ''--------------------------------------------------------
    'Public Function GetSeikyuYM() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT *"
    '        sql += " FROM seikyushime"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While

    '        End Using
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用者情報一覧画面に表示する利用者情報を取得
    '' Input :施設ID、利用者区分
    '' Output:利用者情報
    ''--------------------------------------------------------
    'Public Function GetRiyoshaYMList(ByVal strTantoID As String, Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoshaKbn As String = "") As DataTable

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT s.shisetsu_id, s.shisetsu_nm, r.riyosha_id, r.riyosha_nm, r.riyosha_kbn, k.kbn_nm, s.riyo_nyuryoku_kbn, ky.kbn_nm AS riyo_kbn_nm"
    '        sql += " FROM SHISETSU AS s"
    '        sql += " INNER JOIN SANSHOSHISETSU AS ss ON s.shisetsu_id = ss.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS r ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN KUBUN AS k ON r.riyosha_kbn = k.kbn_id AND k.kbn_shurui = '003'"
    '        sql += " INNER JOIN KUBUN AS ky ON s.riyo_nyuryoku_kbn = ky.kbn_id AND ky.kbn_shurui = '004'"
    '        sql += " WHERE (r.riyosha_kbn = '001' OR r.riyosha_kbn = '002' OR r.riyosha_kbn = '003' OR r.riyosha_kbn = '004')"
    '        sql += "   AND ss.tanto_id = @tanto_id"
    '        If strShisetsuID <> "" Then
    '            sql += "   AND s.shisetsu_id = @shisetsu_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND r.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        sql += " ORDER BY s.shisetsu_id, r.riyosha_kbn, r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@tanto_id", strTantoID))
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ''--------------------------------------------------------
    '' 食数管理表に出力する情報取得
    '' Input :施設ID、利用者区分、利用年月
    '' Output:利用者情報
    ''--------------------------------------------------------
    'Public Function GetShokusuKanri(ByVal strShisetsuID As String, Optional ByVal strRiyoshaKbn As String = "", Optional ByVal strRiyoYM As String = "") As DataTable

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.riyosha_id, rs.riyosha_nm, rs.riyosha_kbn, r.riyo_d, r.riyo_timezone,"
    '        sql += " r.riyo_kbn, r.riyosha_kbn AS riyo_shurui, r.shisetsu_id, s.shisetsu_nm, r.riyo_ym"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " WHERE h.option_kbn = '0' AND r.shisetsu_id = @shisetsu_id  "
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND rs.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strRiyoYM <> "" Then
    '            sql += "   AND r.riyo_ym = @riyo_ym"
    '        End If
    '        sql += " ORDER BY rs.riyosha_kbn, r.riyosha_id, r.riyo_timezone, r.riyo_d "

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用データを取得する
    '' Input :施設ID、利用年月
    '' Output:利用年月の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataList(Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoYM As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT SHISETSUTANKA.shisetsu_id,"
    '        sql += " SHISETSUTANKA.riyosha_kbn,"
    '        sql += " SHISETSUTANKA.hinmoku_id,"
    '        sql += " ry.riyosha_id AS riyo_riyosha_id,"
    '        sql += " SHISETSU.riyo_nyuryoku_kbn,"
    '        sql += " SHISETSU.haishoku_shurui,"
    '        sql += " RIYOSHA.riyosha_id,"
    '        sql += " RIYOSHA.kaishi_ymd,"
    '        sql += " RIYOSHA.kaishi_timezone,"
    '        sql += " RIYOSHA.chushi_ymd,"
    '        sql += " RIYOSHA.chushi_timezone,"
    '        sql += " RIYOSHA.kyushi_kaishi_ymd,"
    '        sql += " RIYOSHA.kyushi_shuryo_ymd,"
    '        sql += " RIYOSHA.kyushi_kaishi_timezone,"
    '        sql += " RIYOSHA.kyushi_shuryo_timezone,"
    '        sql += " RIYOSHA.shiharai_houhou_id,"
    '        sql += " RIYOSHA.kesshoku_youbi,"
    '        sql += " RIYOSHA.kesshoku_haishoku_shurui,"
    '        sql += " RIYOSHA.day_riyo_youbi"
    '        sql += " FROM SHISETSUTANKA, SHISETSU, RIYOSHA"
    '        sql += " LEFT JOIN "
    '        sql += " (SELECT DISTINCT RIYO.riyosha_id FROM RIYO, RIYOSHA, SHISETSUTANKA"
    '        sql += " WHERE RIYOSHA.riyosha_id = RIYO.riyosha_id"
    '        sql += " AND RIYO.shisetsu_id  = SHISETSUTANKA.shisetsu_id"
    '        sql += " AND SHISETSUTANKA.shisetsu_id = @shisetsu_id2"
    '        sql += " AND RIYO.hinmoku_id = SHISETSUTANKA.hinmoku_id"
    '        If strRiyoYM <> "" Then
    '            sql += " AND RIYO.riyo_ym = @riyo_ym"
    '        End If
    '        sql += " AND RIYO.hinmoku_id = SHISETSUTANKA.hinmoku_id"
    '        sql += ") AS ry ON RIYOSHA.riyosha_id = ry.riyosha_id"
    '        sql += " WHERE (SHISETSUTANKA.riyosha_kbn = '001' OR SHISETSUTANKA.riyosha_kbn = '002')"
    '        sql += " AND (SHISETSU.riyo_nyuryoku_kbn = '001')"
    '        sql += " AND RIYOSHA.shisetsu_id = SHISETSUTANKA.shisetsu_id"
    '        sql += " AND RIYOSHA.jotai_kbn = '1'"
    '        sql += " AND RIYOSHA.riyosha_kbn = SHISETSUTANKA.riyosha_kbn"
    '        sql += " AND RIYOSHA.hinmoku_id = SHISETSUTANKA.hinmoku_id"
    '        sql += " AND SHISETSU.shisetsu_id = SHISETSUTANKA.shisetsu_id"
    '        If strShisetsuID <> "" Then
    '            sql += "   AND SHISETSUTANKA.shisetsu_id = @shisetsu_id"
    '        End If
    '        sql += " ORDER BY SHISETSUTANKA.shisetsu_id, SHISETSUTANKA.riyosha_kbn, ry.riyosha_id, SHISETSUTANKA.hinmoku_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id2", strShisetsuID))
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用テーブル登録
    ' ''' </summary>
    ' ''' <param name="ht">Hashtable</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function InsertRiyo(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '利用者テーブルの登録処理 ------------------------------------------------
    '        sql = "INSERT INTO RIYO ("
    '        sql += " shisetsu_id,"
    '        sql += " riyosha_id,"
    '        sql += " riyo_ym,"
    '        sql += " riyo_d,"
    '        sql += " riyo_timezone,"
    '        sql += " riyosha_kbn,"
    '        sql += " hinmoku_id,"
    '        sql += " riyo_kbn,"
    '        sql += " suryo,"
    '        sql += " shiharai_houhou_id,"
    '        sql += " ins_dt,"
    '        sql += " ins_user_id,"
    '        sql += " upd_dt,"
    '        sql += " upd_user_id "
    '        sql += ") VALUES ("
    '        sql += " @shisetsu_id,"
    '        sql += " @riyosha_id,"
    '        sql += " @riyo_ym,"
    '        sql += " @riyo_d,"
    '        sql += " @riyo_timezone,"
    '        sql += " @riyosha_kbn,"
    '        sql += " @hinmoku_id,"
    '        sql += " @riyo_kbn,"
    '        sql += " @suryo,"
    '        sql += " @shiharai_houhou_id,"
    '        sql += " @ins_dt,"
    '        sql += " @ins_user_id,"
    '        sql += " @upd_dt,"
    '        sql += " @upd_user_id"
    '        sql += ")"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim insdt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", ht("shisetsu_id")))     '施設ID
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", ht("riyosha_id")))       '利用者ID
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", ht("riyo_ym")))             '利用年月
    '            cmd.Parameters.Add(New SqlParameter("@riyo_d", ht("riyo_d")))               '利用日
    '            cmd.Parameters.Add(New SqlParameter("@riyo_timezone", ht("riyo_timezone"))) '利用時間帯
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", ht("riyosha_kbn")))     '利用者区分
    '            cmd.Parameters.Add(New SqlParameter("@hinmoku_id", ht("hinmoku_id")))       '品目
    '            cmd.Parameters.Add(New SqlParameter("@riyo_kbn", ht("riyo_kbn")))           '利用区分
    '            cmd.Parameters.Add(New SqlParameter("@suryo", ht("suryo")))                 '数量
    '            cmd.Parameters.Add(New SqlParameter("@shiharai_houhou_id", ht("shiharai_houhou_id")))    '支払方法ID
    '            cmd.Parameters.Add(New SqlParameter("@ins_dt", insdt))                       '作成日
    '            cmd.Parameters.Add(New SqlParameter("@ins_user_id", ht("user_id")))          '作成者
    '            cmd.Parameters.Add(New SqlParameter("@upd_dt", insdt))                       '更新日
    '            cmd.Parameters.Add(New SqlParameter("@upd_user_id", ht("user_id")))          '更新者

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try
    'End Function

    ''--------------------------------------------------------
    '' 利用項目を取得する
    '' Input :施設ID、利用者、利用者区分、品目、利用年月、実費負担品
    '' Output:利用項目の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataYMList(ByVal strShisetsuID As String, _
    '                                  ByVal strRiyoshaID As String, _
    '                                  ByVal strRiyoshaKbn As String, _
    '                                  ByVal strHinmokuID As String, _
    '                                  ByVal strRiyoYM As String, _
    '                                  ByVal strOption As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable
    '    Dim strRiyosha As String = ""

    '    Try
    '        sql = "SELECT RIYO.riyo_d,"
    '        sql += " RIYO.riyo_timezone,"
    '        sql += " RIYO.hinmoku_id,"
    '        sql += " h.hinmoku_nm,"
    '        sql += " RIYO.riyo_kbn,"
    '        sql += " RIYO.riyosha_kbn,"
    '        sql += " RIYO.shiharai_houhou_id"
    '        sql += " FROM RIYO"
    '        sql += " INNER JOIN HINMOKU AS h ON RIYO.hinmoku_id = h.hinmoku_id"
    '        sql += " WHERE RIYO.shisetsu_id = @shisetsu_id"
    '        sql += " AND RIYO.riyosha_id = @riyosha_id"
    '        If strRiyoshaKbn = "001" And strOption = "0" Then
    '            sql += " AND (RIYO.riyosha_kbn = '001' OR RIYO.riyosha_kbn = '002')"
    '        ElseIf strRiyoshaKbn <> "" Then
    '            sql += " AND RIYO.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strHinmokuID <> "" Then
    '            sql += " AND RIYO.hinmoku_id = @hinmoku_id"
    '        End If
    '        If strRiyoYM <> "" Then
    '            sql += " AND RIYO.riyo_ym = @riyo_ym"
    '        End If
    '        If strOption <> "" Then
    '            sql += " AND h.option_kbn = @option_kbn"
    '        End If
    '        sql += " ORDER BY RIYO.riyo_d, RIYO.riyosha_id, RIYO.riyo_timezone"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            If strRiyoshaKbn <> "001" Or strOption <> "0" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strHinmokuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@hinmoku_id", strHinmokuID))
    '            End If
    '            If strOption <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@option_kbn", strOption))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            Dim col As DataColumn = New DataColumn()
    '            col.ColumnName = "date_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "morning_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "lunch_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "snack_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "dinner_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "riyo_d"
    '            col.DataType = GetType(Integer)
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id1"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id2"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id3"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id4"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "shiharai_houhou_id"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 3
    '            dt.Columns.Add(col)

    '            Dim oldDate As Integer = 0
    '            '型付データテーブルに値を設定
    '            Dim haishokuShurui As String = "0000"
    '            Dim row = dt.NewRow
    '            Dim bRead As Boolean = False
    '            While rd.Read
    '                bRead = True
    '                Dim nDate As Integer = rd.GetValue(0)
    '                Dim riyoTimezone As Integer = rd.GetValue(1)
    '                Dim riyoKbn As String = rd.GetValue(4)
    '                Dim riyoshaKbn As String = rd.GetValue(5)
    '                If oldDate = 0 Then
    '                    oldDate = nDate
    '                End If
    '                If nDate <> oldDate Then
    '                    For i As Integer = 1 To 4
    '                        If Mid(haishokuShurui, i, 1) = "0" Then
    '                            row(i) = "　"
    '                            row(i + 5) = ""
    '                        End If
    '                    Next i
    '                    dt.Rows.Add(row)
    '                    haishokuShurui = "0000"
    '                    row = dt.NewRow
    '                    oldDate = nDate
    '                End If
    '                '曜日付き文字列
    '                row(0) = nDate.ToString() + DateConv.GetWeekStr(strRiyoYM, nDate)
    '                If rd.GetValue(2) <> "" Then
    '                    For i As Integer = 1 To 4
    '                        If i = riyoTimezone Then
    '                            Mid(haishokuShurui, i, 1) = "1"
    '                            '実費負担品
    '                            If strOption = "1" Then
    '                                If riyoKbn = "0" Then
    '                                    row(i) = "×"
    '                                Else
    '                                    row(i) = "○"
    '                                End If
    '                            Else
    '                                If riyoKbn = "1" Then           '利用する
    '                                    If riyoshaKbn = "001" Then  '入所者
    '                                        row(i) = rd.GetValue(3)
    '                                    ElseIf riyoshaKbn = "002" Then
    '                                        If strRiyoshaKbn = "001" Then
    '                                            row(i) = "D:" + rd.GetValue(3)
    '                                        Else
    '                                            row(i) = rd.GetValue(3)
    '                                        End If
    '                                    Else
    '                                        row(i) = rd.GetValue(3)
    '                                    End If
    '                                Else
    '                                    row(i) = "×"
    '                                End If
    '                            End If
    '                            row(i + 5) = rd.GetValue(2)
    '                            Exit For
    '                        End If
    '                    Next i
    '                End If
    '                row(5) = oldDate
    '                row(10) = rd.GetValue(6)
    '            End While
    '            If bRead = True Then
    '                For i As Integer = 1 To 4
    '                    If Mid(haishokuShurui, i, 1) = "0" Then
    '                        row(i) = "　"
    '                        row(i + 5) = ""
    '                    End If
    '                Next i
    '                dt.Rows.Add(row)
    '            End If
    '        End Using

    '        '足りない分空レコード作成
    '        Dim nLastDate As Integer = Date.DaysInMonth(Mid(strRiyoYM, 1, 4), Mid(strRiyoYM, 5, 2))
    '        For n As Integer = 1 To nLastDate
    '            Dim bFound As Boolean = False
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i).Item("riyo_d") = n.ToString() Then
    '                    bFound = True
    '                    Exit For
    '                End If
    '            Next i
    '            If bFound = False Then
    '                Dim row = dt.NewRow
    '                row(0) = n.ToString() + DateConv.GetWeekStr(strRiyoYM, n)
    '                For i As Integer = 1 To 4
    '                    row(i) = "　"
    '                    row(i + 5) = ""
    '                Next i
    '                row(5) = n
    '                dt.Rows.Add(row)
    '            End If
    '        Next n

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用項目を取得する
    '' Input :施設ID、利用者、利用年月、利用者区分
    '' Output:利用項目の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataSList(ByVal strShisetsuID As String, _
    '                                 ByVal strRiyoshaID As String, _
    '                                 ByVal strRiyoYM As String, _
    '                                 ByVal strRiyoTime As String, _
    '                                 ByVal strRiyosyaKbn As String, _
    '                                 ByVal dtcol As DataTable) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT RIYO.riyo_d,"
    '        sql += " HINMOKU.hinmoku_id,"
    '        sql += " HINMOKU.hinmoku_nm,"
    '        sql += " RIYO.suryo,"
    '        sql += " RIYONINZU.ninzu"
    '        sql += " FROM RIYO, HINMOKU, RIYONINZU"
    '        sql += " WHERE RIYO.hinmoku_id = HINMOKU.hinmoku_id"
    '        sql += " AND HINMOKU.option_kbn = '0'"
    '        sql += " AND RIYO.shisetsu_id = RIYONINZU.shisetsu_id"
    '        sql += " AND RIYO.riyosha_id = RIYONINZU.riyosha_id"
    '        sql += " AND RIYO.riyosha_kbn = RIYONINZU.riyosha_kbn"
    '        sql += " AND RIYO.riyo_ym = RIYONINZU.riyo_ym"
    '        sql += " AND RIYO.riyo_d = RIYONINZU.riyo_d"
    '        sql += " AND RIYO.riyo_timezone = RIYONINZU.riyo_timezone"
    '        sql += " AND RIYO.shisetsu_id = @shisetsu_id"
    '        sql += " AND RIYO.riyosha_id = @riyosha_id"
    '        sql += " AND RIYO.riyosha_kbn = @riyosha_kbn"
    '        sql += " AND RIYO.riyo_ym = @riyo_ym"
    '        sql += " AND RIYO.riyo_timezone = @riyo_timezone"
    '        sql += " ORDER BY RIYO.riyo_d, RIYO.hinmoku_id"

    '        Dim colCount As Integer = 0
    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyosyaKbn))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_timezone", strRiyoTime))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            Dim col As DataColumn = New DataColumn()
    '            col.ColumnName = "date_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            For i = 0 To dtcol.Rows.Count - 1
    '                col = New DataColumn()
    '                col.ColumnName = "suryo" + i.ToString()
    '                col.DataType = GetType(String)
    '                col.MaxLength = 20
    '                dt.Columns.Add(col)
    '            Next i
    '            col = New DataColumn()
    '            col.ColumnName = "ninzu"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "riyo_d"
    '            col.DataType = GetType(Integer)
    '            dt.Columns.Add(col)

    '            Dim oldDate As Integer = 0

    '            '型付データテーブルに値を設定
    '            Dim row = dt.NewRow
    '            Dim bRead As Boolean = False
    '            While rd.Read
    '                bRead = True
    '                Dim nDate As Integer = rd.GetValue(0)
    '                If oldDate = 0 Then
    '                    oldDate = nDate
    '                End If
    '                If nDate <> oldDate Then
    '                    dt.Rows.Add(row)
    '                    row = dt.NewRow
    '                    oldDate = nDate
    '                End If
    '                '曜日付き文字列
    '                row(0) = nDate.ToString() + DateConv.GetWeekStr(strRiyoYM, nDate)
    '                For i = 0 To dtcol.Rows.Count - 1
    '                    If dtcol.Rows(i).Item(0).ToString() = rd.GetValue(1) Then
    '                        row(i + 1) = rd.GetValue(3)
    '                        Exit For
    '                    End If
    '                Next i
    '                row(dtcol.Rows.Count + 1) = rd.GetValue(4)
    '                row(dtcol.Rows.Count + 2) = oldDate
    '            End While
    '            If bRead = True Then
    '                dt.Rows.Add(row)
    '            End If
    '        End Using

    '        '足りない分空レコード作成
    '        Dim nLastDate As Integer = Date.DaysInMonth(Mid(strRiyoYM, 1, 4), Mid(strRiyoYM, 5, 2))
    '        For n As Integer = 1 To nLastDate
    '            Dim bFound As Boolean = False
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i).Item("riyo_d").ToString() = n.ToString() Then
    '                    bFound = True
    '                    Exit For
    '                End If
    '            Next i
    '            If bFound = False Then
    '                Dim row = dt.NewRow
    '                row(0) = n.ToString() + DateConv.GetWeekStr(strRiyoYM, n)
    '                For i = 1 To dtcol.Rows.Count
    '                    row(i) = ""
    '                Next i
    '                row(dtcol.Rows.Count + 1) = ""
    '                row(dtcol.Rows.Count + 2) = n
    '                dt.Rows.Add(row)
    '            End If
    '        Next n

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' メイン品目利用データを取得する
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strRiyoYMD">利用日</param>
    ' ''' <param name="strRiyoTimezone">利用時間帯</param>
    ' ''' <returns>利用情報データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoDataMain(ByVal strRiyoshaID As String, ByVal strRiyoYMD As String, ByVal strRiyoTimezone As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.*"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id AND h.option_kbn = '0'"
    '        sql += " WHERE r.riyosha_id = @riyosha_id"
    '        sql += "   AND r.riyo_ym + RIGHT('00' + CONVERT(varchar, r.riyo_d), 2) = @riyo_ymd"
    '        sql += "   AND r.riyo_timezone = @riyo_timezone"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ymd", strRiyoYMD))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_timezone", strRiyoTimezone))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用項目（初期値）を取得する
    '' Input :利用者、利用年月
    '' Output:利用項目の一覧
    ''--------------------------------------------------------
    'Public Function GetDefaultList(ByVal strRiyoshaID As String, ByVal strRiyoYM As String, ByVal strHaishokuShurui As String) As DataTable

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT RIYOSHA.hinmoku_id,"
    '        sql += " HINMOKU.hinmoku_nm,"
    '        sql += " RIYOSHA.kaishi_ymd,"
    '        sql += " RIYOSHA.kaishi_timezone,"
    '        sql += " RIYOSHA.chushi_ymd,"
    '        sql += " RIYOSHA.chushi_timezone,"
    '        sql += " RIYOSHA.kyushi_kaishi_ymd,"
    '        sql += " RIYOSHA.kyushi_shuryo_ymd,"
    '        sql += " RIYOSHA.day_riyo_youbi,"
    '        sql += " RIYOSHA.kesshoku_youbi,"
    '        sql += " RIYOSHA.kesshoku_haishoku_shurui,"
    '        sql += " RIYOSHA.kyushi_kaishi_timezone,"
    '        sql += " RIYOSHA.kyushi_shuryo_timezone"
    '        sql += " FROM RIYOSHA, HINMOKU"
    '        sql += " WHERE RIYOSHA.hinmoku_id = HINMOKU.hinmoku_id"
    '        sql += " AND RIYOSHA.riyosha_id = @riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            Dim col As DataColumn = New DataColumn()
    '            col.ColumnName = "date_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "morning_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "lunch_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "snack_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "dinner_nm"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 20
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "riyo_d"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 2
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id1"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id2"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id3"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            col = New DataColumn()
    '            col.ColumnName = "hinmoku_id4"
    '            col.DataType = GetType(String)
    '            col.MaxLength = 6
    '            dt.Columns.Add(col)

    '            Dim oldDate As Integer = 1
    '            '型付データテーブルに値を設定
    '            Dim bRead As Boolean = False
    '            While rd.Read
    '                Dim nLastDate As Integer = Date.DaysInMonth(Mid(strRiyoYM, 1, 4), Mid(strRiyoYM, 5, 2))
    '                For i As Integer = 1 To nLastDate
    '                    Dim row = dt.NewRow
    '                    '曜日付き文字列
    '                    row(0) = i.ToString() + DateConv.GetWeekStr(strRiyoYM, i)
    '                    Dim riyoYMD As String = strRiyoYM + String.Format("{0:D2}", i)
    '                    Dim wDate As DateTime = New DateTime(Mid(riyoYMD, 1, 4), Mid(riyoYMD, 5, 2), Mid(riyoYMD, 7, 2))
    '                    For j As Integer = 1 To 4
    '                        Dim strStartD As String = rd.GetValue(2).ToString()
    '                        Dim strStartT As String = rd.GetValue(3).ToString()
    '                        Dim strEndD As String = rd.GetValue(4).ToString()
    '                        Dim strEndT As String = rd.GetValue(5).ToString()
    '                        Dim strStart As String = rd.GetValue(6).ToString()
    '                        Dim strStartKT As String = rd.GetValue(11).ToString()
    '                        Dim strEnd As String = rd.GetValue(7).ToString()
    '                        Dim strEndKT As String = rd.GetValue(12).ToString()
    '                        Dim strWeek As String = rd.GetValue(8).ToString()
    '                        Dim strKWeek As String = rd.GetValue(9).ToString()
    '                        Dim strHaisyoku As String = rd.GetValue(10).ToString()

    '                        '開始日、開始時間帯
    '                        If strStartD <> "" And strStartT <> "" And (riyoYMD < strStartD Or (riyoYMD = strStartD And j.ToString() < strStartT)) Then
    '                            row(j + 5) = ""
    '                            row(j) = "　"
    '                        ElseIf strEndD <> "" And strEndT <> "" And (riyoYMD > strEndD Or (riyoYMD = strEndD And j.ToString() >= strEndT)) Then
    '                            row(j + 5) = rd.GetValue(0)
    '                            row(j) = "×"
    '                        ElseIf strStart <> "" And strEnd <> "" And strStartKT <> "" And strEndKT <> "" And _
    '                            ((riyoYMD > strStart Or (riyoYMD = strStart And j.ToString() >= strStartKT)) And (riyoYMD < strEnd Or (riyoYMD = strEnd And j.ToString() <= strEndKT))) Then
    '                            row(j + 5) = rd.GetValue(0)
    '                            row(j) = "×"
    '                        ElseIf Mid(strKWeek, wDate.DayOfWeek + 1, 1) = "1" And Mid(strHaisyoku, j, 1) = "1" Then
    '                            row(j + 5) = rd.GetValue(0)
    '                            row(j) = "×"
    '                        ElseIf Mid(strWeek, wDate.DayOfWeek + 1, 1) = "1" Then
    '                            '朝食と夕食は001（通常）で作成する
    '                            If j = 1 Or j = 4 Then
    '                                row(j) = rd.GetValue(1)
    '                                row(j + 5) = rd.GetValue(0)
    '                            Else
    '                                row(j) = "D:" + rd.GetValue(1)
    '                                row(j + 5) = rd.GetValue(0)
    '                            End If
    '                        Else
    '                            row(j) = rd.GetValue(1)
    '                            row(j + 5) = rd.GetValue(0)
    '                        End If
    '                        If Mid(strHaishokuShurui, j, 1) = "0" Then
    '                            row(j + 5) = ""
    '                            row(j) = "　"
    '                        End If
    '                    Next j
    '                    row(5) = i
    '                    dt.Rows.Add(row)
    '                Next i
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' メニューリストを取得
    '' Input :施設ID、利用者区分、オプション区分
    '' Output:メニューリスト情報
    ''--------------------------------------------------------
    'Public Function GetMenuList(ByVal strShisetsuID As String, ByVal strRiyoshaKbn As String, ByVal strOption As String, ByVal strRiyoNyuryokuKbn As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT HINMOKU.hinmoku_id, HINMOKU.hinmoku_nm, SHISETSUTANKA.riyosha_kbn"
    '        sql += " FROM HINMOKU, SHISETSUTANKA"
    '        sql += " INNER JOIN SHISETSU As s ON s.shisetsu_id = SHISETSUTANKA.shisetsu_id AND s.riyo_nyuryoku_kbn = @riyo_nyuryoku_kbn"
    '        sql += " WHERE HINMOKU.hinmoku_id = SHISETSUTANKA.hinmoku_id"
    '        sql += " AND HINMOKU.option_kbn = @option_kbn"
    '        sql += " AND SHISETSUTANKA.shisetsu_id = @shisetsu_id"

    '        If strRiyoshaKbn = "001" Then
    '            sql += " AND (SHISETSUTANKA.riyosha_kbn = '001' OR SHISETSUTANKA.riyosha_kbn = '002')"
    '        ElseIf strRiyoshaKbn <> "" Then
    '            sql += " AND SHISETSUTANKA.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        sql += " ORDER BY HINMOKU.hinmoku_id, SHISETSUTANKA.riyosha_kbn"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@option_kbn", strOption))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_nyuryoku_kbn", strRiyoNyuryokuKbn))
    '            If strRiyoshaKbn <> "" And strRiyoshaKbn <> "001" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            Dim schema As DataTable = rd.GetSchemaTable()
    '            For Each row As DataRow In schema.Rows
    '                Dim col As DataColumn = New DataColumn()
    '                col.ColumnName = row("ColumnName").ToString()
    '                col.DataType = Type.GetType(row("DataType").ToString())

    '                If col.DataType.Equals(GetType(String)) Then
    '                    If col.ColumnName = "hinmoku_id" Then
    '                        If strRiyoshaKbn = "001" Then
    '                            col.MaxLength = 10
    '                        Else
    '                            col.MaxLength = Integer.Parse(row("ColumnSize"))
    '                        End If
    '                    Else
    '                        col.MaxLength = Integer.Parse(row("ColumnSize"))
    '                    End If
    '                End If
    '                dt.Columns.Add(col)
    '            Next

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                If strRiyoshaKbn = "001" Then
    '                    row(0) = rd.GetValue(0) + rd.GetValue(2).ToString()
    '                Else
    '                    row(0) = rd.GetValue(0)
    '                End If
    '                If rd.GetValue(2).ToString() = "001" Then
    '                    row(1) = rd.GetValue(1)
    '                ElseIf rd.GetValue(2).ToString() = "002" Then
    '                    If strRiyoshaKbn = "001" Then
    '                        row(1) = "D:" + rd.GetValue(1)
    '                    Else
    '                        row(1) = rd.GetValue(1)
    '                    End If
    '                Else
    '                    row(1) = rd.GetValue(1)
    '                End If
    '                dt.Rows.Add(row)
    '            End While
    '        End Using
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 渡された利用年月の利用情報を削除する
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <param name="strHinmokuID">品目ID</param>
    ' ''' <param name="strRiyoYM">利用年月</param>
    ' ''' <param name="strRiyoTime">利用時間帯</param>
    ' ''' <param name="strOption">実費負担品</param>
    ' ''' <returns>削除した件数</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyoYM(ByVal strShisetsuID As String, _
    '                             ByVal strRiyoshaID As String, _
    '                             ByVal strRiyoshaKbn As String, _
    '                             ByVal strHinmokuID As String, _
    '                             ByVal strRiyoYM As String, _
    '                             ByVal strRiyoTime As String, _
    '                             ByVal strOption As String) As Integer

    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '利用テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE r FROM RIYO AS r"
    '        If strHinmokuID <> "" Or strOption <> "" Then
    '            sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id"
    '        End If
    '        sql += " WHERE r.shisetsu_id = @shisetsu_id"
    '        sql += " AND r.riyosha_id = @riyosha_id"
    '        If strOption <> "" Then
    '            sql += " AND h.option_kbn = @option_kbn"
    '        End If
    '        If strHinmokuID <> "" Then
    '            sql += " AND r.hinmoku_id = @hinmoku_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += " AND r.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strRiyoTime <> "" Then
    '            sql += " AND r.riyo_timezone = @riyo_timezone"
    '        End If

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))
    '            If strHinmokuID <> "" And strOption = "1" Then
    '                cmd.Parameters.Add(New SqlParameter("hinmoku_id", strHinmokuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_ym", strRiyoYM))
    '            End If
    '            If strRiyoTime <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_timezone", strRiyoTime))
    '            End If
    '            If strOption <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("option_kbn", strOption))
    '            End If

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        Return Cnt

    '    Catch
    '        Throw

    '    End Try
    'End Function

    ''--------------------------------------------------------
    '' 品目IDを取得
    '' Input :品目名
    '' Output:品目ID
    ''--------------------------------------------------------
    'Public Function GetHinmokuID(ByVal strHinmokuNM As String) As String

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim strHinmokuID As String = ""

    '    Try
    '        sql = "SELECT HINMOKU.hinmoku_id"
    '        sql += " FROM HINMOKU"
    '        sql += " WHERE HINMOKU.hinmoku_nm = @hinmoku_nm"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@hinmoku_nm", Trim(strHinmokuNM)))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                strHinmokuID = rd.GetValue(0).ToString()
    '            End While

    '        End Using
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return strHinmokuID

    'End Function

    ''--------------------------------------------------------
    '' 施設単価マスタから施設の最小品目IDを取得
    '' Input :施設ID
    '' Output:品目ID
    ''--------------------------------------------------------
    'Public Function GetMinHinmokuID(ByVal strShisetsuID As String, ByVal strRiyoshaKbn As String) As String

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim strHinmokuID As String = ""

    '    Try
    '        sql = "SELECT MIN(hinmoku_id)"
    '        sql += " FROM SHISETSUTANKA"
    '        sql += " WHERE shisetsu_id = @shisetsu_id"
    '        sql += "   AND riyosha_kbn = @riyosha_kbn"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", Trim(strShisetsuID)))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", Trim(strRiyoshaKbn)))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                strHinmokuID = rd.GetValue(0).ToString()
    '            End While

    '        End Using
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return strHinmokuID

    'End Function

    ' ''' <summary>
    ' ''' 請求締管理テーブルより現在年月を取得
    ' ''' </summary>
    ' ''' <returns>現在年月</returns>
    ' ''' <remarks></remarks>
    'Public Function GetGenzaiYM() As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT genzai_ym"
    '        sql += " FROM SEIKYUSHIME"
    '        sql += " WHERE id = '1'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'SQL実行
    '            Return cmd.ExecuteScalar.ToString
    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 請求締管理テーブルより入力可を取得
    ' ''' </summary>
    ' ''' <returns>入力可</returns>
    ' ''' <remarks></remarks>
    'Public Function GetNyuryokuOK() As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT nyuryoku_ok"
    '        sql += " FROM SEIKYUSHIME"
    '        sql += " WHERE id = '1'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'SQL実行
    '            Return cmd.ExecuteScalar.ToString
    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 請求め管理テーブルを更新
    ' ''' </summary>
    ' ''' <param name="strNyuryoku">入力可("1":入力可、"0":入力不可</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function UpdateSeikyushime(ByVal strNyuryoku As String) As Boolean
    '    Dim sql As String
    '    Dim rc As Integer

    '    Try
    '        '更新処理
    '        'SQL文の設定
    '        sql = "UPDATE SEIKYUSHIME"
    '        sql += " SET nyuryoku_ok = @nyuryoku_ok"
    '        sql += " WHERE id = '1'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@nyuryoku_ok", strNyuryoku))

    '            '更新
    '            rc = cmd.ExecuteNonQuery()

    '        End Using

    '        If rc = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw New ApplicationException
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 請求締管理テーブルを更新
    ' ''' </summary>
    ' ''' <param name="strGenzaiYM">現在年月</param>
    ' ''' <param name="strNyuryoku">入力可("1":入力可、"0":入力不可</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function UpdateSeikyushime(ByVal strGenzaiYM As String, ByVal strNyuryoku As String) As Boolean
    '    Dim sql As String
    '    Dim rc As Integer

    '    Try
    '        '更新処理
    '        'SQL文の設定
    '        sql = "UPDATE SEIKYUSHIME SET"
    '        sql += " genzai_ym = @genzai_ym,"
    '        sql += " nyuryoku_ok = @nyuryoku_ok"
    '        sql += " WHERE id = '1'"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@genzai_ym", strGenzaiYM))
    '            cmd.Parameters.Add(New SqlParameter("@nyuryoku_ok", strNyuryoku))

    '            '更新
    '            rc = cmd.ExecuteNonQuery()

    '        End Using

    '        If rc = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw New ApplicationException
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 現在年月の請求明細を削除する
    ' ''' </summary>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteSeikyumeisai() As Boolean
    '    Dim sql As String
    '    Dim rc As Integer

    '    Try
    '        '更新処理
    '        'SQL文の設定
    '        sql = "DELETE SEIKYUMEISAI"
    '        sql += " WHERE seikyu_ym = (SELECT genzai_ym FROM SEIKYUSHIME WHERE ID = '1')"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            '更新
    '            rc = cmd.ExecuteNonQuery()

    '        End Using

    '        Return True

    '    Catch ex As Exception
    '        Throw New ApplicationException
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 請求データ作成用に利用データを集計して取得（内部利用者用）
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <returns>利用集計データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoDataForSeikyuNaibu(ByVal strSeikyuYM As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id,"
    '        sql += " SUM("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN '1' THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN '2' THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN '3' THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN '4' THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " WHERE r.riyo_ym = @riyo_ym1"
    '        sql += "   AND ((r.riyosha_kbn = '001' OR r.riyosha_kbn = '002') OR (r.riyosha_kbn = '004' AND s.seikyu_kbn = '1'))"
    '        'sql += "   AND rs.jotai_kbn = '1'"
    '        sql += "   AND r.riyo_kbn = '1'"
    '        sql += "   AND r.shiharai_houhou_id = '003'"
    '        sql += " GROUP BY r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id"
    '        sql += " ORDER BY r.shisetsu_id, r.riyosha_id"

    '        'sql = "SELECT r.shisetsu_id, '1' AS atesaki, 1 AS gyo_no, 'ご利用者お食事分' AS koumoku,"
    '        'sql += " SUM("
    '        'sql += "    CASE r.riyo_timezone"
    '        'sql += "        WHEN '1' THEN"
    '        'sql += "	        CASE s.seikyu_tanka_kbn"
    '        'sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        'sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        'sql += "            END"
    '        'sql += "        WHEN '2' THEN"
    '        'sql += "	        CASE s.seikyu_tanka_kbn"
    '        'sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        'sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        'sql += "            END"
    '        'sql += "        WHEN '3' THEN"
    '        'sql += "	        CASE s.seikyu_tanka_kbn"
    '        'sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        'sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        'sql += "            END"
    '        'sql += "        WHEN '4' THEN"
    '        'sql += "	        CASE s.seikyu_tanka_kbn"
    '        'sql += "	            WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        'sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        'sql += "            END"
    '        'sql += "    END"
    '        'sql += " ) AS kingaku"
    '        'sql += " FROM RIYO AS r"
    '        'sql += " INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        'sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        'sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        'sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        'sql += " WHERE r.riyo_ym = @riyo_ym1"
    '        'sql += "   AND ((r.riyosha_kbn = '001' OR r.riyosha_kbn = '002') OR (r.riyosha_kbn = '004' AND s.seikyu_kbn = '1'))"
    '        'sql += "   AND rs.jotai_kbn = '1'"
    '        'sql += "   AND r.riyo_kbn = '1'"
    '        'sql += "   AND r.shiharai_houhou_id = '003'"
    '        'sql += " GROUP BY r.shisetsu_id"
    '        'sql += " UNION"
    '        'sql += " SELECT s.shisetsu_id, '1' AS atesaki, 2 AS gyo_no, '固定費負担分' AS koumoku,"
    '        'sql += " IsNull(s.koteihi_futan_kin, 0) AS kingaku"
    '        'sql += " FROM SHISETSU AS s"
    '        'sql += " WHERE EXISTS(SELECT 1 FROM RIYO AS r"
    '        'sql += "                INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        'sql += "                INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        'sql += "                WHERE r.shisetsu_id = s.shisetsu_id"
    '        'sql += "                  AND r.riyo_ym = @riyo_ym2"
    '        'sql += "                  AND ((r.riyosha_kbn = '001' OR r.riyosha_kbn = '002') OR (r.riyosha_kbn = '004' AND s.seikyu_kbn = '1'))"
    '        'sql += "                  AND rs.jotai_kbn = '1'"
    '        'sql += "                  AND r.riyo_kbn = '1'"
    '        'sql += "                  AND r.shiharai_houhou_id = '003'"
    '        'sql += "                  AND s.koteihi_kbn = '1'"
    '        'sql += " )"
    '        'sql += " ORDER BY shisetsu_id, gyo_no"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym1", strSeikyuYM))
    '            'cmd.Parameters.Add(New SqlParameter("@riyo_ym2", strSeikyuYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求データ作成用に施設の固定費負担分を取得
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <returns>固定費負担分</returns>
    ' ''' <remarks></remarks>
    'Public Function GetKoteihiForSeikyuNaibu(ByVal strSeikyuYM As String, ByVal strShisetsuID As String) As Integer
    '    Dim sql As String

    '    Try
    '        sql = "SELECT IsNull(s.koteihi_futan_kin, 0)"
    '        sql += " FROM SHISETSU AS s"
    '        sql += " WHERE s.shisetsu_id = @shisetsu_id"
    '        sql += "   AND EXISTS(SELECT 1 FROM RIYO AS r"
    '        sql += "                INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        sql += "                INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += "                WHERE r.shisetsu_id = s.shisetsu_id"
    '        sql += "                  AND r.riyo_ym = @riyo_ym"
    '        sql += "                  AND ((r.riyosha_kbn = '001' OR r.riyosha_kbn = '002') OR (r.riyosha_kbn = '004' AND s.seikyu_kbn = '1'))"
    '        'sql += "                  AND rs.jotai_kbn = '1'"
    '        sql += "                  AND r.riyo_kbn = '1'"
    '        sql += "                  AND s.koteihi_kbn = '1'"
    '        sql += " )"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strSeikyuYM))

    '            'SQL実行
    '            Return cmd.ExecuteScalar

    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 請求データ作成用に利用データを集計して取得（職員用）
    ' ''' </summary>
    ' ''' <returns>利用集計データテーブル</returns>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoDataForSeikyuShokuin(ByVal strSeikyuYM As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id,"
    '        sql += " SUM("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN 3 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN 4 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " WHERE r.riyo_ym = @riyo_ym"
    '        sql += "   AND r.riyosha_kbn = '003'"
    '        'sql += "   AND rs.jotai_kbn = '1'"
    '        sql += "   AND r.riyo_kbn = '1'"
    '        sql += "   AND r.shiharai_houhou_id = '003'"
    '        sql += " GROUP BY r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id"
    '        sql += " ORDER BY r.shisetsu_id, r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strSeikyuYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求データ作成用に利用データを集計して取得（付き添い者用）
    ' ''' </summary>
    ' ''' <returns>利用集計データテーブル</returns>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoDataForSeikyuTsukisoi(ByVal strSeikyuYM As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id,"
    '        sql += " SUM("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN 3 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN 4 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " WHERE r.riyo_ym = @riyo_ym"
    '        sql += "   AND r.riyosha_kbn = '004'"
    '        sql += "   AND s.seikyu_kbn = '0'"
    '        'sql += "   AND rs.jotai_kbn = '1'"
    '        sql += "   AND r.riyo_kbn = '1'"
    '        sql += "   AND r.shiharai_houhou_id = '003'"
    '        sql += " GROUP BY r.shisetsu_id, s.seikyu_tanka_kbn, r.riyosha_id"
    '        sql += " ORDER BY r.shisetsu_id, r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strSeikyuYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求明細テーブル登録
    ' ''' </summary>
    ' ''' <param name="ht">Hashtable</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function InsertSeikyuMeisai(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '請求明細テーブルの登録処理 ------------------------------------------------
    '        sql = "INSERT INTO SEIKYUMEISAI ("
    '        sql += " seikyu_ym, shisetsu_id, atesaki, gyo_no, koumoku, kingaku, ins_dt, ins_user_id, upd_dt, upd_user_id"
    '        sql += ") VALUES ("
    '        sql += " @seikyu_ym, @shisetsu_id, @atesaki, @gyo_no, @koumoku, @kingaku, @ins_dt, @ins_user_id, @upd_dt, @upd_user_id"
    '        sql += ")"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim insdt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@seikyu_ym", ht("seikyu_ym")))         '請求年月
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", ht("shisetsu_id")))     '施設ID
    '            cmd.Parameters.Add(New SqlParameter("@atesaki", ht("atesaki")))             '宛先
    '            cmd.Parameters.Add(New SqlParameter("@gyo_no", ht("gyo_no")))               '行番号
    '            cmd.Parameters.Add(New SqlParameter("@koumoku", ht("koumoku")))             '項目
    '            cmd.Parameters.Add(New SqlParameter("@kingaku", ht("kingaku")))             '金額
    '            cmd.Parameters.Add(New SqlParameter("@ins_dt", insdt))                      '作成日
    '            cmd.Parameters.Add(New SqlParameter("@ins_user_id", ht("user_id")))         '作成者
    '            cmd.Parameters.Add(New SqlParameter("@upd_dt", insdt))                      '更新日
    '            cmd.Parameters.Add(New SqlParameter("@upd_user_id", ht("user_id")))         '更新者

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 請求明細テーブルより請求年月の一覧を取得
    ' ''' </summary>
    ' ''' <returns>請求年月データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetSeikyuYMList() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT seikyu_ym,"
    '        sql += " CASE WHEN seikyu_ym  > '19890107' THEN '平成' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( seikyu_ym, 1, 4 ) AS INT) - 1988) + '年' + SUBSTRING( seikyu_ym, 5, 2 ) + '月'"
    '        sql += " WHEN seikyu_ym  > '19261224' then '昭和' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( seikyu_ym, 1, 4 ) AS INT) - 1925) + '年' + SUBSTRING( seikyu_ym, 5, 2 ) + '月'"
    '        sql += " WHEN seikyu_ym  > '19120729' then '大正' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( seikyu_ym, 1, 4 ) AS INT) - 1911) + '年' + SUBSTRING( seikyu_ym, 5, 2 ) + '月'"
    '        sql += " WHEN seikyu_ym  > '18680124' then '明治' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( seikyu_ym, 1, 4 ) AS INT) - 1867) + '年' + SUBSTRING( seikyu_ym, 5, 2 ) + '月'"
    '        sql += " END seikyu_ymH"
    '        sql += " FROM SEIKYUMEISAI"
    '        sql += " ORDER BY seikyu_ym DESC"

    '        Using cmd As New SqlCommand(sql, cn)
    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求明細情報を取得
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者("1":入所者・デイサービス、"2":職員、"3":付き添い者)</param>
    ' ''' <returns>請求明細情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetSeikyuMeisaiList(ByVal strSeikyuYM As String, ByVal strShisetsuID As String, ByVal strRiyoshaKbn As String, Optional ByVal intGyoNo As Integer = -1) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT sm.*, ss.shisetsu_nm"
    '        sql += " FROM SEIKYUMEISAI AS sm"
    '        sql += " INNER JOIN SHISETSU AS ss ON sm.shisetsu_id = ss.shisetsu_id"
    '        sql += " WHERE sm.seikyu_ym = @seikyu_ym"
    '        sql += "   AND sm.shisetsu_id = @shisetsu_id"
    '        sql += "   AND sm.atesaki = @atesaki"
    '        If intGyoNo <> -1 Then
    '            sql += "   AND sm.gyo_no = @gyo_no"
    '        End If
    '        sql += " ORDER BY sm.gyo_no"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@seikyu_ym", strSeikyuYM))
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@atesaki", strRiyoshaKbn))
    '            If intGyoNo <> -1 Then
    '                cmd.Parameters.Add(New SqlParameter("@gyo_no", intGyoNo))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求明細テーブル更新
    ' ''' </summary>
    ' ''' <param name="ht">ハッシュテーブル</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function UpdateSeikyuMeisai(ByVal ht As Hashtable) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '請求明細テーブルの更新処理 ------------------------------------------------
    '        sql = "UPDATE SEIKYUMEISAI SET"
    '        sql += " koumoku = @koumoku,"
    '        sql += " kingaku = @kingaku,"
    '        sql += " upd_dt = @upd_dt,"
    '        sql += " upd_user_id = @upd_user_id"
    '        sql += " WHERE seikyu_ym = @seikyu_ym"
    '        sql += "   AND shisetsu_id = @shisetsu_id"
    '        sql += "   AND atesaki = @atesaki"
    '        sql += "   AND gyo_no = @gyo_no"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim upddt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("koumoku", ht("koumoku")))
    '            cmd.Parameters.Add(New SqlParameter("kingaku", ht("kingaku")))
    '            cmd.Parameters.Add(New SqlParameter("upd_dt", upddt))
    '            cmd.Parameters.Add(New SqlParameter("upd_user_id", ht("user_id")))
    '            cmd.Parameters.Add(New SqlParameter("seikyu_ym", ht("seikyu_ym")))
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", ht("shisetsu_id")))
    '            cmd.Parameters.Add(New SqlParameter("atesaki", ht("atesaki")))
    '            cmd.Parameters.Add(New SqlParameter("gyo_no", ht("gyo_no")))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 請求明細テーブルを削除
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strAtesaki">宛先</param>
    ' ''' <param name="intGyoNo">行番号</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteSeikyuMeisai(ByVal strSeikyuYM As String, ByVal strShisetsuID As String, ByVal strAtesaki As String, ByVal intGyoNo As Integer) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try

    '        '請求明細テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE SEIKYUMEISAI"
    '        sql += " WHERE seikyu_ym = @seikyu_ym"
    '        sql += "   AND shisetsu_id = @shisetsu_id"
    '        sql += "   AND atesaki = @atesaki"
    '        sql += "   AND gyo_no = @gyo_no"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("seikyu_ym", strSeikyuYM))
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("atesaki", strAtesaki))
    '            cmd.Parameters.Add(New SqlParameter("gyo_no", intGyoNo))

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw New ApplicationException

    '    End Try
    'End Function
    ''--------------------------------------------------------
    '' 施設毎の利用者数を取得
    '' Input :施設ID
    '' Output:利用者
    ''--------------------------------------------------------
    'Public Function GetRiyoshasu(ByVal strShisetsuID As String, ByVal strRiyoshaKbn As String, ByVal strRiyoYM As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT COUNT(DISTINCT r.riyosha_id) AS riyosha_su "
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " WHERE h.option_kbn = '0' AND r.shisetsu_id = @shisetsu_id  "
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND rs.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strRiyoYM <> "" Then
    '            sql += "   AND r.riyo_ym = @riyo_ym"
    '        End If

    '        Using cmd As New SqlCommand(sql, cn)
    '            'パラメータの追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If


    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While

    '        End Using
    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ' ''' <summary>
    ' ''' 食札出力用データを取得
    ' ''' </summary>
    ' ''' <param name="ShisetsuID">施設ID</param>
    ' ''' <param name="RiyoshaKBN">利用者区分</param>
    ' ''' <returns>利用者情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetShokusatsuData(ByVal ShisetsuID As Integer, ByVal RiyoshaKBN As Integer) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.riyosha_nm, r.riyosha_id, s.shisetsu_nm, s.shisetsu_id, "
    '        sql += " k0.kbn_id AS shokushu_id ,k0.kbn_nm AS shokushu_nm, "
    '        sql += " k1.kbn_id AS shokuji_keitai_id, k1.kbn_nm AS shokuji_keitai, r.shokuji_keitai_etc, "
    '        sql += " k2.kbn_id AS shushoku_id, k2.kbn_nm AS shushoku, r.shushoku_etc, r.shushokuryo,"
    '        sql += " r.shokusatsu1, r.shokusatsu2, r.shokusatsu3, r.shokusatsu4,day_riyo_youbi,"
    '        sql += " k3.kbn_id, k3.kbn_nm AS choshoku_main, k4.kbn_id AS choshoku_option_id, k4.kbn_nm AS choshoku_option,"
    '        sql += " r.choshoku_option_etc, r.riyosha_kbn"
    '        sql += " FROM RIYOSHA AS r"
    '        sql += " LEFT OUTER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " LEFT OUTER JOIN KUBUN AS k0 ON r.shokushu_id = k0.kbn_id AND k0.kbn_shurui = '001' "
    '        sql += " LEFT OUTER JOIN KUBUN AS k1 ON r.shokuji_keitai_id = k1.kbn_id AND k1.kbn_shurui = '002' "
    '        sql += " LEFT OUTER JOIN KUBUN AS k2 ON r.shushoku_id = k2.kbn_id AND k2.kbn_shurui = '005' "
    '        sql += " LEFT OUTER JOIN KUBUN AS k3 ON r.choshoku_main_kbn = k3.kbn_id AND k3.kbn_shurui = '006' "
    '        sql += " LEFT OUTER JOIN KUBUN AS k4 ON r.choshoku_option_kbn = k4.kbn_id AND k4.kbn_shurui = '007'"
    '        sql += " WHERE r.shisetsu_id = @shisetsu_id"
    '        sql += " AND r.riyosha_kbn = @riyosha_kbn"
    '        sql += " AND r.jotai_kbn = '1'"
    '        sql += " ORDER BY r.riyosha_kbn, r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", ShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", RiyoshaKBN))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 利用者名コンボボックスに表示する利用者名を取得
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <param name="strRiyoYM">利用年月</param>
    ' ''' <returns>利用者名情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoshaNMList(Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoshaKbn As String = "", Optional ByVal strRiyoYM As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT r.riyosha_id, r.riyosha_nm"
    '        sql += " FROM RIYOSHA AS r"
    '        sql += " INNER JOIN SHISETSU AS s ON s.shisetsu_id = r.shisetsu_id"
    '        sql += " INNER JOIN RIYO AS ry ON r.riyosha_id = ry.riyosha_id"
    '        sql += " WHERE ry.riyo_ym = @riyo_ym"
    '        If strShisetsuID <> "" Then
    '            sql += "   AND r.shisetsu_id = @shisetsu_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += "   AND r.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        sql += " ORDER BY r.riyosha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用データExcel用を取得する
    '' Input :施設ID、利用年月
    '' Output:利用年月の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataExcel(Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoshaKbn As String = "", Optional ByVal strRiyoshaID As String = "", Optional ByVal strRiyoYM As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT "
    '        sql += " r.shisetsu_id,"
    '        sql += " r.riyosha_kbn, "
    '        sql += " s.riyo_nyuryoku_kbn,"
    '        sql += " s.seikyu_tanka_kbn,"
    '        sql += " r.riyosha_id,"
    '        sql += " r.riyo_ym,"
    '        sql += " r.riyo_d,"
    '        sql += " r.suryo,"
    '        sql += " r.riyo_timezone,"
    '        sql += " RIYONINZU.ninzu,"
    '        sql += " ("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN 3 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN 4 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) * r.suryo AS kingaku"
    '        sql += " FROM RIYO As r"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id"
    '        sql += " AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " LEFT JOIN RIYONINZU ON"
    '        sql += " (r.riyo_ym = RIYONINZU.riyo_ym"
    '        sql += " AND r.riyo_d = RIYONINZU.riyo_d"
    '        sql += " AND r.riyo_timezone = RIYONINZU.riyo_timezone"
    '        sql += " AND r.riyosha_kbn = RIYONINZU.riyosha_kbn"
    '        sql += " AND r.riyosha_id = RIYONINZU.riyosha_id"
    '        sql += " AND r.shisetsu_id = RIYONINZU.shisetsu_id)"
    '        sql += " WHERE r.riyo_kbn = '1'"
    '        sql += " AND r.riyosha_kbn = st.riyosha_kbn"
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strShisetsuID <> "" Then
    '            sql += "   AND r.shisetsu_id = @shisetsu_id"
    '        End If
    '        If strRiyoshaKbn <> "" Then
    '            sql += " AND rs.riyosha_kbn = @riyosha_kbn"
    '        End If
    '        If strRiyoshaID <> "" Then
    '            sql += "   AND rs.riyosha_id = @riyosha_id"
    '        End If
    '        sql += " ORDER BY"
    '        sql += " r.shisetsu_id,"
    '        sql += " r.riyosha_id,"
    '        sql += " r.riyosha_kbn,"
    '        sql += " r.riyo_ym,"
    '        sql += " r.riyo_d,"
    '        sql += " r.riyo_timezone"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strRiyoshaID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用データ（施設宛）Excel用を取得する
    '' Input :施設ID、利用年月
    '' Output:利用年月の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataSExcel(Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoYM As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.shisetsu_id,"
    '        sql += " r.riyosha_id,"
    '        sql += " r.riyo_ym,"
    '        sql += " r.riyo_d,"
    '        sql += " s.seikyu_tanka_kbn,"
    '        sql += " r.suryo,"
    '        sql += " ("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN 3 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN 4 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) * r.suryo AS kingaku"
    '        sql += " FROM RIYO As r"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id"
    '        sql += " AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " WHERE r.riyosha_kbn = st.riyosha_kbn"
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strShisetsuID <> "" Then
    '            sql += "   AND r.shisetsu_id = @shisetsu_id"
    '        End If
    '        sql += " ORDER BY r.shisetsu_id, r.riyo_ym, r.riyo_d"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ''--------------------------------------------------------
    '' 利用データ個人宛（入所者）Excel用を取得する
    '' Input :施設ID、利用年月
    '' Output:利用年月の一覧
    ''--------------------------------------------------------
    'Public Function GetRiyoDataKExcel(Optional ByVal strShisetsuID As String = "", Optional ByVal strRiyoshaKbn As String = "", Optional ByVal strRiyoshaID As String = "", Optional ByVal strRiyoYM As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT r.shisetsu_id,"
    '        sql += " r.riyosha_kbn,"
    '        sql += " s.riyo_nyuryoku_kbn,"
    '        sql += " s.seikyu_tanka_kbn,"
    '        sql += " r.riyosha_id,"
    '        sql += " r.riyo_ym,"
    '        sql += " r.riyo_d,"
    '        sql += " r.riyo_timezone,"
    '        sql += " r.riyo_kbn,"
    '        sql += " ("
    '        sql += "    CASE r.riyo_timezone"
    '        sql += "        WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE st.tanka_zeikomi_kin1"
    '        sql += "            END"
    '        sql += "        WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE st.tanka_zeikomi_kin2"
    '        sql += "            END"
    '        sql += "        WHEN 3 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN st.tanka_zeinuki_kin3"
    '        sql += "	            ELSE st.tanka_zeikomi_kin3"
    '        sql += "            END"
    '        sql += "        WHEN 4 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' then st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE st.tanka_zeikomi_kin4"
    '        sql += "            END"
    '        sql += "    END"
    '        sql += " ) AS kingaku,"
    '        sql += " s.haishoku_shurui,"
    '        sql += " rs.riyosha_kbn AS riyosha_riyosha_kbn"
    '        sql += " FROM RIYO As r"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id"
    '        sql += " AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        'sql += " WHERE r.riyo_kbn = '1'"
    '        sql += " WHERE r.riyosha_kbn = st.riyosha_kbn"
    '        sql += " AND rs.riyosha_kbn = @riyosha_kbn"
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strShisetsuID <> "" Then
    '            sql += "   AND r.shisetsu_id = @shisetsu_id"
    '        End If
    '        If strRiyoshaKbn = "001" Then
    '            sql += " AND (r.riyosha_kbn = '001' OR r.riyosha_kbn = '002')"
    '        End If
    '        If strRiyoshaID <> "" Then
    '            sql += "   AND rs.riyosha_id = @riyosha_id"
    '        End If
    '        sql += " ORDER BY r.shisetsu_id, r.riyosha_id, r.riyo_ym, r.riyo_d, r.hinmoku_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If
    '            If strRiyoshaKbn <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))
    '            End If
    '            If strRiyoshaID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            End If
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 消費税率を取得する
    ' ''' </summary>
    ' ''' <param name="strYMD">日付</param>
    ' ''' <returns>消費税率</returns>
    ' ''' <remarks></remarks>
    'Public Function GetShohiZei(ByVal strYMD As String) As Double
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT  zeiritsu"
    '        sql += " FROM SHOHIZEIRITSU"
    '        sql += " WHERE kaishi_ymd <= @ymd"
    '        sql += " AND shuryo_ymd >= @ymd"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@ymd", strYMD))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    '小数点以下2桁
    '    Dim dCoef As Double = System.Math.Pow(10, 2)
    '    Dim dValue As Double = CDbl(dt.Rows(0).Item(0).ToString()) / 100.0

    '    Return System.Math.Ceiling(dValue * dCoef) / dCoef

    'End Function

    ' ''' <summary>
    ' ''' 会社情報を取得する
    ' ''' </summary>
    ' ''' <param name="strID">ID</param>
    ' ''' <returns>会社情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetKaishaInfo(ByVal strID As String) As DataTable

    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT *"
    '        sql += " FROM KAISHA"
    '        sql += " WHERE kaisha_id = @kaisha_id"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@kaisha_id", strID))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 請求書出力用に請求明細データを取得する
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <returns>請求明細データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetSeikyuDataExcel(ByVal strSeikyuYM As String, Optional ByVal strShisetsuID As String = "") As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT sm.shisetsu_id,"
    '        sql += " sm.atesaki,"
    '        sql += " sm.gyo_no,"
    '        sql += " sm.koumoku,"
    '        sql += " sm.kingaku,"
    '        sql += " ss.shisetsu_nm,"
    '        sql += " ss.riyo_nyuryoku_kbn,"
    '        sql += " ss.seikyu_tanka_kbn"
    '        sql += " FROM SEIKYUMEISAI As sm"
    '        sql += " INNER JOIN SHISETSU AS ss ON sm.shisetsu_id = ss.shisetsu_id"
    '        sql += " WHERE sm.seikyu_ym = @seikyu_ym"
    '        If strShisetsuID <> "" Then
    '            sql += "  AND sm.shisetsu_id = @shisetsu_id"
    '        End If
    '        sql += " ORDER BY sm.shisetsu_id, sm.atesaki, sm.gyo_no"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@seikyu_ym", strSeikyuYM))
    '            If strShisetsuID <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ' ''' <summary>
    ' ''' 食数管理表（集計）出力用データを取得
    ' ''' </summary>
    ' ''' <param name="ShisetsuID">施設ID</param>
    ' ''' <param name="RiyoYM">利用年月</param>
    ' ''' <returns>利用者情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetShokusushukeiData(ByVal ShisetsuID As String, ByVal RiyoYM As Integer) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable


    '    Try
    '        sql = "SELECT r.shisetsu_id, s.shisetsu_nm, s.haishoku_shurui,s.seikyu_tanka_kbn, r.riyo_d, r.riyo_timezone,"
    '        sql += "      rs.riyosha_kbn AS riyosha_kbn, r.riyosha_kbn AS riyo_shurui, sum(r.suryo) as suryo,"
    '        sql += " SUM("
    '        sql += "   CASE r.riyo_timezone"
    '        sql += "       WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            End"
    '        sql += "       WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            End"
    '        sql += "        WHEN 3 THEN"
    '        sql += "			CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "                ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            End"
    '        sql += "        WHEN 4 THEN"
    '        sql += "            CASE s.seikyu_tanka_kbn"
    '        sql += "                WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            End"
    '        sql += "   End"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id "
    '        sql += "  AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id and h.option_kbn = '0'"
    '        sql += " WHERE r.riyo_ym = @riyo_ym"
    '        sql += " AND (r.riyosha_kbn = '001' OR r.riyosha_kbn = '002')"
    '        sql += " AND r.riyo_kbn = '1'"
    '        sql += " AND r.shisetsu_id= @shisetsu_id"
    '        sql += " AND s.riyo_nyuryoku_kbn = '001' "
    '        sql += " GROUP BY r.shisetsu_id, r.riyo_d, r.riyo_timezone,rs.riyosha_kbn, r.riyosha_kbn, "
    '        sql += "          s.shisetsu_nm, s.haishoku_shurui, s.seikyu_tanka_kbn "
    '        sql += " UNION "
    '        sql += " SELECT riyo_shukei.shisetsu_id, riyo_shukei.shisetsu_nm, riyo_shukei.haishoku_shurui,riyo_shukei.seikyu_tanka_kbn, riyo_shukei.riyo_d, riyo_shukei.riyo_timezone,"
    '        sql += "      riyo_shukei.riyosha_kbn, riyo_shukei.riyo_shurui, SUM(rn.ninzu) AS suryo, riyo_shukei.kingaku "
    '        sql += " FROM RIYONINZU AS rn "
    '        sql += " INNER JOIN "
    '        sql += " (SELECT r.shisetsu_id, s.shisetsu_nm, s.haishoku_shurui,s.seikyu_tanka_kbn, r.riyo_d, r.riyo_timezone,"
    '        sql += "      rs.riyosha_kbn AS riyosha_kbn, r.riyosha_kbn AS riyo_shurui, r.riyo_ym, r.riyosha_id, "
    '        sql += " SUM("
    '        sql += "   CASE r.riyo_timezone"
    '        sql += "       WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            End"
    '        sql += "       WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            End"
    '        sql += "        WHEN 3 THEN"
    '        sql += "			CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "                ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            End"
    '        sql += "        WHEN 4 THEN"
    '        sql += "            CASE s.seikyu_tanka_kbn"
    '        sql += "                WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            End"
    '        sql += "   End"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id "
    '        sql += "  AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id and h.option_kbn = '0' "
    '        sql += " WHERE r.riyo_ym = @riyo_ym"
    '        sql += " AND (r.riyosha_kbn = '001' OR r.riyosha_kbn = '002')"
    '        sql += " AND r.riyo_kbn = '1'"
    '        sql += " AND r.shisetsu_id= @shisetsu_id"
    '        sql += " AND s.riyo_nyuryoku_kbn = '002' "
    '        sql += " GROUP BY r.shisetsu_id, r.riyo_d, r.riyo_timezone,rs.riyosha_kbn, r.riyosha_kbn, "
    '        sql += "          s.shisetsu_nm, s.haishoku_shurui, s.seikyu_tanka_kbn, r.riyo_ym, r.riyosha_id) AS riyo_shukei "
    '        sql += " ON rn.shisetsu_id = riyo_shukei.shisetsu_id AND rn.riyosha_id = riyo_shukei.riyosha_id "
    '        sql += " AND rn.riyo_ym = riyo_shukei.riyo_ym AND rn.riyo_d = riyo_shukei.riyo_d "
    '        sql += " AND rn.riyo_timezone = riyo_shukei.riyo_timezone AND rn.riyosha_kbn = riyo_shukei.riyosha_kbn "
    '        sql += " GROUP BY riyo_shukei.shisetsu_id, riyo_shukei.shisetsu_nm, riyo_shukei.haishoku_shurui,riyo_shukei.seikyu_tanka_kbn, riyo_shukei.riyo_d, riyo_shukei.riyo_timezone, "
    '        sql += "          riyo_shukei.riyosha_kbn, riyo_shukei.riyo_shurui,riyo_shukei.kingaku "
    '        sql += " ORDER BY r.shisetsu_id, r.riyo_d, r.riyo_timezone, rs.riyosha_kbn, r.riyosha_kbn"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", ShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", RiyoYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ' ''' <summary>
    ' ''' 食数管理表（集計）出力用データ(option)を取得
    ' ''' </summary>
    ' ''' <param name="ShisetsuID">施設ID</param>
    ' ''' <param name="RiyoYM">利用年月</param>
    ' ''' <returns>利用者情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetOptionshukeiData(ByVal ShisetsuID As String, ByVal RiyoYM As Integer) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable


    '    Try
    '        sql = "SELECT r.shisetsu_id, s.shisetsu_nm, s.haishoku_shurui,s.seikyu_tanka_kbn, r.riyo_d, r.riyo_timezone,"
    '        sql += "      rs.riyosha_kbn AS riyosha_kbn, r.riyosha_kbn AS riyo_shurui, h.hinmoku_id, h.hinmoku_nm, sum(r.suryo) as suryo,"
    '        sql += " SUM("
    '        sql += "   CASE r.riyo_timezone"
    '        sql += "       WHEN 1 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin1"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin1"
    '        sql += "            End"
    '        sql += "       WHEN 2 THEN"
    '        sql += "	        CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin2"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin2"
    '        sql += "            End"
    '        sql += "        WHEN 3 THEN"
    '        sql += "			CASE s.seikyu_tanka_kbn"
    '        sql += "	            WHEN '1' THEN r.suryo * st.tanka_zeinuki_kin3"
    '        sql += "                ELSE r.suryo * st.tanka_zeikomi_kin3"
    '        sql += "            End"
    '        sql += "        WHEN 4 THEN"
    '        sql += "            CASE s.seikyu_tanka_kbn"
    '        sql += "                WHEN '1' then r.suryo * st.tanka_zeinuki_kin4"
    '        sql += "	            ELSE r.suryo * st.tanka_zeikomi_kin4"
    '        sql += "            End"
    '        sql += "   End"
    '        sql += " ) AS kingaku"
    '        sql += " FROM RIYO AS r"
    '        sql += " INNER JOIN SEIKYUSHIME AS ss ON r.riyo_ym = ss.genzai_ym"
    '        sql += " INNER JOIN SHISETSU AS s ON r.shisetsu_id = s.shisetsu_id"
    '        sql += " INNER JOIN RIYOSHA AS rs ON r.riyosha_id = rs.riyosha_id"
    '        sql += " INNER JOIN SHISETSUTANKA AS st ON r.shisetsu_id = st.shisetsu_id "
    '        sql += "  AND r.riyosha_kbn = st.riyosha_kbn AND r.hinmoku_id = st.hinmoku_id"
    '        sql += " INNER JOIN HINMOKU AS h ON r.hinmoku_id = h.hinmoku_id AND h.option_kbn = '1'"
    '        sql += " WHERE r.riyo_ym = @riyo_ym"
    '        sql += " AND (r.riyosha_kbn = '001' OR r.riyosha_kbn = '002')"
    '        sql += " AND r.riyo_kbn = '1'"
    '        sql += " AND r.shisetsu_id= @shisetsu_id"
    '        sql += " GROUP BY r.shisetsu_id, r.riyo_d, r.riyo_timezone,rs.riyosha_kbn, r.riyosha_kbn, "
    '        sql += "          s.shisetsu_nm, s.haishoku_shurui, s.seikyu_tanka_kbn, h.hinmoku_id, h.hinmoku_nm "
    '        sql += " ORDER BY h.hinmoku_id, r.shisetsu_id, r.riyo_d, r.riyo_timezone, rs.riyosha_kbn, r.riyosha_kbn"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", ShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", RiyoYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function
    ' ''' <summary>
    ' ''' 利用年月を取得
    ' ''' </summary>
    ' ''' <returns>利用年月</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoYM() As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT DISTINCT r.riyo_ym,"
    '        sql += " Case when r.riyo_ym  > '19890107' then '平成' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1988) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '19261224' then '昭和' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1925) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '19120729' then '大正' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1911) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " when r.riyo_ym  > '18680124' then '明治' + CONVERT( VARCHAR( 5 ), CAST(SUBSTRING( r.riyo_ym, 1, 4 ) AS INT) - 1867) + '年' + SUBSTRING( r.riyo_ym, 5, 2 ) + '月'"
    '        sql += " end riyo_ymH"
    '        sql += " FROM RIYO AS r"
    '        sql += " ORDER BY r.riyo_ym"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function


    ' ''' <summary>
    ' ''' 請求一覧出力用に請求明細データを取得する
    ' ''' </summary>
    ' ''' <param name="strSeikyuYM">請求年月</param>
    ' ''' <returns>請求明細データテーブル</returns>
    ' ''' <remarks></remarks>
    'Public Function GetSeikyuListExcel(ByVal strSeikyuYM As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        sql = "SELECT sm.seikyu_ym,"
    '        sql += " sm.shisetsu_id,"
    '        sql += " ss.shisetsu_nm,"
    '        sql += " ss.seikyu_tanka_kbn,"
    '        sql += " sm.atesaki,"
    '        sql += " SUM(sm.kingaku) AS kingaku"
    '        sql += " FROM SEIKYUMEISAI As sm"
    '        sql += " INNER JOIN SHISETSU AS ss ON sm.shisetsu_id = ss.shisetsu_id"
    '        sql += " WHERE sm.seikyu_ym = @seikyu_ym"
    '        sql += " GROUP BY sm.seikyu_ym, sm.shisetsu_id, ss.shisetsu_nm, ss.seikyu_tanka_kbn, sm.atesaki"
    '        sql += " ORDER BY sm.shisetsu_id, sm.atesaki"

    '        Using cmd As New SqlCommand(sql, cn)

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@seikyu_ym", strSeikyuYM))

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While
    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの最終利用日を取得する
    ' ''' </summary>
    ' ''' <returns>最終利用日</returns>
    ' ''' <remarks></remarks>
    'Public Function GetLastRiyoDate(ByVal strRiyoshaID As String) As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT riyo_ym + RIGHT('00' + CONVERT(varchar, MAX(riyo_d)), 2)"
    '        sql += " FROM RIYO AS r"
    '        sql += " WHERE riyosha_id = @riyosha_id"
    '        sql += "   AND riyo_ym = (SELECT MAX(riyo_ym) FROM riyo WHERE riyosha_id = r.riyosha_id)"
    '        sql += " GROUP BY riyo_ym"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))

    '            'SQL実行
    '            Dim v As Object = cmd.ExecuteScalar
    '            If v Is Nothing Then
    '                Return ""
    '            Else
    '                Return v
    '            End If

    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 渡された利用者IDの利用データで、渡された日付、時間帯より後の最初の利用日、時間帯を取得する
    ' ''' </summary>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strKijunDate">基準日</param>
    ' ''' <param name="strKijunTimezone">基準時間帯</param>
    ' ''' <returns>基準日基準時間帯より後の最初の利用日、時間帯(YYYYMMDDX)</returns>
    ' ''' <remarks></remarks>
    'Public Function GetFirstRiyoDate(ByVal strRiyoshaID As String, ByVal strKijunDate As String, ByVal strKijunTimezone As String) As String
    '    Dim sql As String

    '    Try
    '        sql = "SELECT IsNull(MIN(riyo_ym + RIGHT('00' + CONVERT(varchar, riyo_d), 2) + riyo_timezone), '')"
    '        sql += " FROM riyo"
    '        sql += " WHERE riyosha_id = @riyosha_id"
    '        sql += "   AND riyo_kbn = '1'"
    '        sql += "   AND (riyo_ym > @kijunYM1 OR (riyo_ym = @kijunYM2 AND riyo_d > @kijunD1) OR (riyo_ym = @kijunYM3 AND riyo_d = @kijunD2 AND riyo_timezone > @kijunTZ))"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータ追加
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("@kijunYM1", strKijunDate.Substring(0, 6)))
    '            cmd.Parameters.Add(New SqlParameter("@kijunYM2", strKijunDate.Substring(0, 6)))
    '            cmd.Parameters.Add(New SqlParameter("@kijunYM3", strKijunDate.Substring(0, 6)))
    '            cmd.Parameters.Add(New SqlParameter("@kijunD1", Integer.Parse(strKijunDate.Substring(6, 2))))
    '            cmd.Parameters.Add(New SqlParameter("@kijunD2", Integer.Parse(strKijunDate.Substring(6, 2))))
    '            cmd.Parameters.Add(New SqlParameter("@kijunTZ", strKijunTimezone))

    '            'SQL実行
    '            Dim v As Object = cmd.ExecuteScalar
    '            If v Is Nothing Then
    '                Return ""
    '            Else
    '                Return v
    '            End If

    '        End Using

    '    Catch
    '        Throw New ApplicationException

    '    End Try

    'End Function

    ''--------------------------------------------------------
    '' システムログの追加
    ''--------------------------------------------------------
    'Public Function Insert_SysLog(ByVal strIP As String, ByVal strTantoID As String, ByVal strOPUser As String, ByVal strStatus As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '追加処理
    '        sql = "INSERT INTO SYSTEMLOG"
    '        sql += " VALUES (@cl_ipaddr,"
    '        sql += " @tanto_id,"
    '        sql += " @sousa_dt,"
    '        sql += " @sousa_tgt,"
    '        sql += " @status)"

    '        Using cmd As New SqlCommand(sql, cn)

    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            cmd.Parameters.Add(New SqlParameter("@cl_ipaddr", strIP))
    '            cmd.Parameters.Add(New SqlParameter("@tanto_id", strTantoID))
    '            cmd.Parameters.Add(New SqlParameter("@sousa_dt", Now))
    '            cmd.Parameters.Add(New SqlParameter("@sousa_tgt", strOPUser))
    '            cmd.Parameters.Add(New SqlParameter("@status", strStatus))

    '            'システムログの追加
    '            Dim rc As Integer = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw
    '    End Try

    'End Function

    ''--------------------------------------------------------
    '' 担当者のロック更新
    ''--------------------------------------------------------
    'Public Function TantoLock(ByVal strGtantoID As String, ByVal strOPUser As String, ByVal strLockKbn As String) As Boolean
    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '更新処理
    '        'SQL文の設定
    '        sql = "UPDATE TANTO"
    '        sql += " SET lock_kbn = @lock_kbn,"
    '        If strOPUser <> "" Then
    '            sql += " upd_user_id = @Stanto_id,"
    '        End If
    '        sql += " upd_dt = @updateDate"
    '        sql += " WHERE tanto_id = @Gtanto_id"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If

    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@lock_kbn", strLockKbn))
    '            cmd.Parameters.Add(New SqlParameter("@Gtanto_id", strGtantoID))
    '            If strOPUser <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("@Stanto_id", strOPUser))
    '            End If
    '            cmd.Parameters.Add(New SqlParameter("@updateDate", Now))

    '            '担当者マスタの更新
    '            Dim rc As Integer = cmd.ExecuteNonQuery()

    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された年月が請求締更新実行後の年月かを返す
    ' ''' </summary>
    ' ''' <param name="YM">チェックする年月</param>
    ' ''' <returns>結果(true:請求締更新後、false:請求締更新前)</returns>
    ' ''' <remarks></remarks>
    'Public Function IsSeikyuzumiYM(ByVal YM As String, Optional ByVal db As DBAccess = Nothing) As Boolean
    '    Dim NewCon As Boolean = False

    '    If db Is Nothing Then
    '        db = New DBAccess
    '        NewCon = True
    '    End If

    '    Try
    '        'DB接続
    '        If NewCon Then
    '            db.ConnectDB()
    '        End If

    '        Dim GenzaiYM As String = db.GetGenzaiYM
    '        Dim NyuryokuOK As String = db.GetNyuryokuOK
    '        If (YM < GenzaiYM) OrElse (YM = GenzaiYM And NyuryokuOK = "0") Then
    '            Return True
    '        End If

    '    Finally
    '        If NewCon Then
    '            db.DisConnectDB()
    '            db = Nothing
    '        End If
    '    End Try

    '    Return False
    'End Function

    ' ''' <summary>
    ' ''' 利用人数テーブル登録
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <param name="strRiyoYM">利用年月</param>
    ' ''' <param name="strRiyoTime">利用時間帯</param>
    ' ''' <param name="strNinzu">利用人数</param>
    ' ''' <param name="strUser">更新者</param>
    ' ''' <returns>結果（true:成功、false:失敗）</returns>
    ' ''' <remarks></remarks>
    'Public Function InsertRiyoNZ(ByVal strShisetsuID As String, _
    '                             ByVal strRiyoshaID As String, _
    '                             ByVal strRiyoshaKbn As String, _
    '                             ByVal strRiyoYM As String, _
    '                             ByVal strRiyoD As String, _
    '                             ByVal strRiyoTime As String, _
    '                             ByVal strNinzu As String, _
    '                             ByVal strUser As String) As Boolean

    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '利用人数テーブルの登録処理 ------------------------------------------------
    '        sql = "INSERT INTO RIYONINZU ("
    '        sql += " shisetsu_id,"
    '        sql += " riyosha_id,"
    '        sql += " riyo_ym,"
    '        sql += " riyo_d,"
    '        sql += " riyo_timezone,"
    '        sql += " riyosha_kbn,"
    '        sql += " ninzu,"
    '        sql += " ins_dt,"
    '        sql += " ins_user_id,"
    '        sql += " upd_dt,"
    '        sql += " upd_user_id "
    '        sql += ") VALUES ("
    '        sql += " @shisetsu_id,"
    '        sql += " @riyosha_id,"
    '        sql += " @riyo_ym,"
    '        sql += " @riyo_d,"
    '        sql += " @riyo_timezone,"
    '        sql += " @riyosha_kbn,"
    '        sql += " @ninzu,"
    '        sql += " @ins_dt,"
    '        sql += " @ins_user_id,"
    '        sql += " @upd_dt,"
    '        sql += " @upd_user_id"
    '        sql += ")"

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            Dim insdt As DateTime = Now
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("@shisetsu_id", strShisetsuID))     '施設ID
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_id", strRiyoshaID))       '利用者ID
    '            cmd.Parameters.Add(New SqlParameter("@riyo_ym", strRiyoYM))             '利用年月
    '            cmd.Parameters.Add(New SqlParameter("@riyo_d", CInt(strRiyoD)))         '利用日
    '            cmd.Parameters.Add(New SqlParameter("@riyo_timezone", strRiyoTime))     '利用時間帯
    '            cmd.Parameters.Add(New SqlParameter("@ninzu", CInt(strNinzu)))          '利用人数
    '            cmd.Parameters.Add(New SqlParameter("@riyosha_kbn", strRiyoshaKbn))     '利用者区分
    '            cmd.Parameters.Add(New SqlParameter("@ins_dt", insdt))                  '作成日
    '            cmd.Parameters.Add(New SqlParameter("@ins_user_id", strUser))           '作成者
    '            cmd.Parameters.Add(New SqlParameter("@upd_dt", insdt))                  '更新日
    '            cmd.Parameters.Add(New SqlParameter("@upd_user_id", strUser))           '更新者

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        If Cnt = 0 Then
    '            Return False
    '        End If

    '        Return True

    '    Catch
    '        Throw

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された利用年月の人数情報を削除する
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <param name="strRiyoYM">利用年月</param>
    ' ''' <param name="strRiyoD">利用日</param>
    ' ''' <param name="strRiyoTime">利用時間帯</param>
    ' ''' <returns>削除した件数</returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteRiyoNZ(ByVal strShisetsuID As String, _
    '                             ByVal strRiyoshaID As String, _
    '                             ByVal strRiyoshaKbn As String, _
    '                             ByVal strRiyoYM As String, _
    '                             ByVal strRiyoD As String, _
    '                             ByVal strRiyoTime As String) As Integer

    '    Dim sql As String
    '    Dim Cnt As Integer

    '    Try
    '        '利用人数テーブルの削除処理 ------------------------------------------------
    '        sql = "DELETE r FROM RIYONINZU AS r"
    '        sql += " WHERE r.shisetsu_id = @shisetsu_id"
    '        sql += " AND r.riyosha_id = @riyosha_id"
    '        sql += " AND r.riyosha_kbn = @riyosha_kbn"
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strRiyoD <> "" Then
    '            sql += " AND r.riyo_d = @riyo_d"
    '        End If
    '        If strRiyoTime <> "" Then
    '            sql += " AND r.riyo_timezone = @riyo_timezone"
    '        End If

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_kbn", strRiyoshaKbn))
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_ym", strRiyoYM))
    '            End If
    '            If strRiyoD <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_d", strRiyoD))
    '            End If
    '            If strRiyoTime <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_timezone", strRiyoTime))
    '            End If

    '            Cnt = cmd.ExecuteNonQuery()
    '        End Using

    '        Return Cnt

    '    Catch
    '        Throw

    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 渡された利用年月の人数情報を取得する
    ' ''' </summary>
    ' ''' <param name="strShisetsuID">施設ID</param>
    ' ''' <param name="strRiyoshaID">利用者ID</param>
    ' ''' <param name="strRiyoshaKbn">利用者区分</param>
    ' ''' <param name="strRiyoYM">利用年月</param>
    ' ''' <param name="strRiyoD">利用日</param>
    ' ''' <param name="strRiyoTime">利用時間帯</param>
    ' ''' <returns>人数情報</returns>
    ' ''' <remarks></remarks>
    'Public Function GetRiyoNZ(ByVal strShisetsuID As String, _
    '                          ByVal strRiyoshaID As String, _
    '                          ByVal strRiyoshaKbn As String, _
    '                          ByVal strRiyoYM As String, _
    '                          ByVal strRiyoD As String, _
    '                          ByVal strRiyoTime As String) As DataTable
    '    Dim sql As String
    '    Dim rd As SqlDataReader = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        '利用人数の取得処理 ------------------------------------------------
    '        sql = "SELECT * FROM RIYONINZU AS r"
    '        sql += " WHERE r.shisetsu_id = @shisetsu_id"
    '        sql += " AND r.riyosha_id = @riyosha_id"
    '        sql += " AND r.riyosha_kbn = @riyosha_kbn"
    '        If strRiyoYM <> "" Then
    '            sql += " AND r.riyo_ym = @riyo_ym"
    '        End If
    '        If strRiyoD <> "" Then
    '            sql += " AND r.riyo_d = @riyo_d"
    '        End If
    '        If strRiyoTime <> "" Then
    '            sql += " AND r.riyo_timezone = @riyo_timezone"
    '        End If

    '        Using cmd As New SqlCommand(sql, cn)
    '            If tran IsNot Nothing Then
    '                cmd.Transaction = tran
    '            End If
    '            'パラメータを追加
    '            cmd.Parameters.Add(New SqlParameter("shisetsu_id", strShisetsuID))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_id", strRiyoshaID))
    '            cmd.Parameters.Add(New SqlParameter("riyosha_kbn", strRiyoshaKbn))
    '            If strRiyoYM <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_ym", strRiyoYM))
    '            End If
    '            If strRiyoD <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_d", strRiyoD))
    '            End If
    '            If strRiyoTime <> "" Then
    '                cmd.Parameters.Add(New SqlParameter("riyo_timezone", strRiyoTime))
    '            End If

    '            'SQL実行
    '            rd = cmd.ExecuteReader

    '            '型付データテーブルを作成
    '            dt = CreateSchemaDataTable(rd)

    '            '型付データテーブルに値を設定
    '            While rd.Read
    '                Dim row = dt.NewRow
    '                For i As Integer = 0 To rd.FieldCount - 1
    '                    row(i) = rd.GetValue(i)
    '                Next i
    '                dt.Rows.Add(row)
    '            End While

    '        End Using

    '    Catch
    '        Return Nothing

    '    Finally
    '        'データリーダを閉じる
    '        If Not rd Is Nothing AndAlso rd.IsClosed = False Then
    '            rd.Close()
    '        End If
    '    End Try

    '    Return dt

    'End Function

End Class
