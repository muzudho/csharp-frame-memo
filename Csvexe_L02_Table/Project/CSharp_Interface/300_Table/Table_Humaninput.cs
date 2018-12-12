using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;


namespace Xenon.Table
{



    #region 用意
    //────────────────────────────────────────

    public delegate void DELEGATE_Records( Record_Humaninput record, ref bool isBreak, Log_Reports log_Reports);

    //────────────────────────────────────────
    #endregion



    /// <summary>
    /// テーブルです。
    /// 各セルが「Humaninput_～」型になっており、int型の列にも空白文字列などを入力可能になっています。
    /// 
    /// フィールドの型定義と、0～複数件のレコードを持ちます。
    /// </summary>
    public interface Table_Humaninput : Conf_String
    {



        #region アクション
        //────────────────────────────────────────
        //
        // テーブル作成
        //

        /// <summary>
        /// 設定された型リストで、テーブルの構造を作成します。
        /// </summary>
        /// <param name="recordFielddef"></param>
        /// <param name="log_Reports"></param>
        void CreateTable(RecordFielddef recordFielddef, Log_Reports log_Reports);

        /// <summary>
        /// NOフィールドを 0からの連番に振りなおします。
        /// 
        /// NOフィールド値は、プログラム中で主キーとして使うことがあるので、
        /// 変更するのであれば、ファイルを読み込んだ直後にするものとします。
        /// </summary>
        void RenumberingNoField();

        /// <summary>
        /// 「END」フィールドの左に、新しいフィールドを追加します。
        /// 同名の列が既に追加されている場合は無視されます。
        /// </summary>
        /// <param name="fielddefinition_New"></param>
        /// <param name="isRequired">追加に失敗したときエラーにするなら真。ただし、既に同名の列が追加されている場合は除く。</param>
        /// <param name="log_Reports"></param>
        /// <returns>追加に成功した場合、真を返します。</returns>
        bool AddField( Fielddef fielddefinition_New, bool isRequired, Log_Reports log_Reports);

        /// <summary>
        /// フィールドを追加します。
        /// 同名の列が既に追加されている場合は無視されます。
        /// </summary>
        /// <param name="fielddefinition_New"></param>
        /// <param name="isRequired">追加に失敗したときエラーにするなら真。ただし、既に同名の列が追加されている場合は除く。</param>
        /// <param name="log_Reports"></param>
        /// <returns>追加に成功した場合、真を返します。</returns>
        bool InsertField(int columnIndex, Fielddef fielddefinition_New, bool isRequired, Log_Reports log_Reports);

        //────────────────────────────────────────
        //
        // レコード編集
        //

        /// <summary>
        /// レコードを追加します。
        /// </summary>
        /// <param name="record"></param>
        void AddRecord(Record_Humaninput record);

        /// <summary>
        /// 行データを渡すことで、テーブル内容を追加します。
        /// テーブルの型定義と、データを渡します。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="recordFielddef"></param>
        /// <param name="log_Reports"></param>
        void AddRecordList(
            List<List<string>> rows, RecordFielddef recordFielddef, Log_Reports log_Reports);

        /// <summary>
        /// 空レコードを作成します。
        /// </summary>
        /// <param name="sErrorMsg"></param>
        /// <returns></returns>
        Record_Humaninput CreateNewRecord(string config, Log_Reports log_Reports);

        /// <summary>
        /// 行の削除。
        /// </summary>
        /// <param name="dataRow"></param>
        void Remove(Record_Humaninput dataRow);

        /// <summary>
        /// 指定したレコードの並び順を１つ上に上げます。
        /// </summary>
        /// <param name="rowIndex"></param>
        void MoveRecordToUpByIndex(int rowIndex);

        /// <summary>
        /// 指定したレコードの並び順を１つ下に下げます。
        /// </summary>
        /// <param name="rowIndex"></param>
        void MoveRecordToDownByIndex(int rowIndex);

        /// <summary>
        /// 指定項目A（1～複数件）を、指定項目Bの下に移動させます。
        /// </summary>
        /// <param name="sourceIndices">移動待ち要素のリスト。インデックスの昇順に並んでいる必要があります。</param>
        /// <param name="destinationIndex"></param>
        void MoveItemsBefore(int[] sourceIndices, int destinationIndex);

        //────────────────────────────────────────
        //
        // 取得
        //

        /// <summary>
        /// フィールドの定義を取得します。
        /// 
        /// フィールド名の英字大文字、小文字は無視します。
        /// </summary>
        /// <param name="recordFielddef_Out"></param>
        /// <param name="expected_FieldNameList"></param>
        /// <param name="isRequired">該当なしの時に例外を投げるなら真。</param>
        /// <param name="log_Reports"></param>
        /// <returns>該当無しがあれば偽。</returns>
        bool TryGetFieldDefinitionByName(
            out RecordFielddef recordFielddef_Out,
            List<string> expected_FieldNameList,
            bool isRequired,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 指定のstring型フィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="expectedStringParam"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>一致しなければヌル。</returns>
        List<Record_Humaninput> SelectByString(
            string fieldName,
            StringCellImpl expectedStringParam,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        /// <summary>
        /// NOフィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="exceptedNo"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Record_Humaninput SelectByNo(
            IntCellImpl exceptedNo,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 指定のInt型フィールドの値で指定したレコードを返します。なければヌルです。
        /// </summary>
        /// <param name="name_Field"></param>
        /// <param name="expectedInt"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Record_Humaninput SelectByInt(
            string name_Field,
            IntCellImpl expectedInt,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// データパートのレコード。
        /// 上３行を読み飛ばします。
        /// </summary>
        /// <param name="delegate_Records"></param>
        /// <param name="log_Reports"></param>
        void ForEach_Datapart(DELEGATE_Records delegate_Records, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 【2012-08-25 追加】
        /// </summary>
        /// <param name="name_Field"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        bool ContainsField(string name_Field, bool isRequired, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion

        

        #region プロパティー
        //────────────────────────────────────────

        Expr_Filepath Expr_Filepath_ConfigStack
        {
            get;
        }

        RecordFielddef RecordFielddef
        {
            get;
        }

        DataTable DataTable
        {
            get;
            set;
        }

        /// <summary>
        /// テーブルの名前。
        /// </summary>
        string Name_Table
        {
            get;
            set;
        }

        /// <summary>
        /// このテーブルの「テーブル_ユニット名」。なければ空文字列。使ってる？
        /// （NAME_FORM）
        /// (旧：table unit？)
        /// </summary>
        string Tableunit
        {
            get;
            set;
        }

        /// <summary>
        /// 「TYPE_DATA」フィールド値。
        /// 「T:～;」といった文字列が入る。
        /// (フィールド名：TYPE_DATA)
        /// </summary>
        string Typedata
        {
            get;
            set;
        }

        /// <summary>
        /// 「日別バックアップ」を行うなら真。
        /// (date backup)
        /// </summary>
        bool IsDatebackupActivated
        {
            get;
            set;
        }

        /// <summary>
        /// テーブルの内容保存方法などの設定。
        /// </summary>
        Format_Table Format_Table_Humaninput
        {
            get;
            set;
        }

        //────────────────────────────────────────
        #endregion



    }
}
