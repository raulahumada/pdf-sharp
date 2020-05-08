using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSharp.Internal
{
    internal class CustomStyles
    {
        //Constantes Styles
        public const string PrincipalTitle = "PrincipalTitle";
        public const string ColumnHeader = "ColumnHeader";
        public const string ColumnContent = "ColumnContent";
        public const string ColumnHeaderCenter = "ColumnHeaderCenter";
        public const string SecondColumnHeaderCenter = "SecondColumnHeaderCenter";
        public const string ColumnContentCenter = "ColumnContentCenter";
        public const string PrincipalSubtitle = "PrincipalSubtitle";
        public const string PrincipalDateTime = "PrincipalDateTime";
        public const string TitleParagraph = "TitleParagraph";
        public const string ParagraphSmall = "ParagraphSmall";
        public const string ParagraphSmallFirstPage = "ParagraphSmallFirstPage";
        public const string CoveragesList = "CoveragesList";
        public const string CoveragesListBasic = "CoveragesListBasic";
        public const string TextProjection = "TextProjection";
        public const string TextTakeOff = "TextTakeOff";
        public const string TitleParagraphNoBold = "TitleParagraphNoBold";
        public const string ParagraphNormal = "ParagraphNormal";
        public const string FooterText = "FooterText";
        public const string SignatureLeft = "SignatureLeft";
        public const string SignatureRight = "SignatureRight";
        public const string SignatureTextLeft = "SignatureTextLeft";
        public const string LineColor = "LineColor";
        public const string ColumnContentGray = "ColumnContentGray";
        public const string ColumnContentWhite = "ColumnContentWhite";
        public const string FinishRow = "FinishRow";

        public static readonly Color _Zafiro = Color.FromRgb(0, 58, 112);
        public const string SizeDistance = "0.03 in";

        public static void Define(Document doc)
        {

            var heading1 = doc.Styles[StyleNames.Heading1];
            heading1.BaseStyle = StyleNames.Normal;
            heading1.ParagraphFormat.Font = new Font("Lato");
            heading1.Font.Size = 24;
            heading1.ParagraphFormat.SpaceBefore = 20;

            var heading2 = doc.Styles[StyleNames.Heading2];
            heading1.BaseStyle = StyleNames.Normal;
            heading1.ParagraphFormat.Font = new Font("Lato");
            heading2.ParagraphFormat.Shading.Color = Color.FromRgb(0, 0, 0);
            heading2.ParagraphFormat.Font.Color = Color.FromRgb(255, 255, 255);
            heading2.ParagraphFormat.Font.Bold = true;
            heading2.ParagraphFormat.SpaceBefore = 10;
            heading2.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            heading2.ParagraphFormat.RightIndent = Size.TableCellPadding;
            heading2.ParagraphFormat.Borders.Distance = Size.TableCellPadding;


            #region Styles

            //Style Principal Title 
            var principalTitleStyle = doc.Styles.AddStyle(PrincipalTitle, StyleNames.Normal);
            principalTitleStyle.ParagraphFormat.Font = new Font("Montserrat Alternates Semibold");
            principalTitleStyle.ParagraphFormat.Font.Size = 13;

            // Style Principal Subtitle
            var principalSubtitle = doc.Styles.AddStyle(PrincipalSubtitle, StyleNames.Normal);
            principalSubtitle.ParagraphFormat.Font = new Font("Montserrat Alternates");
            principalSubtitle.ParagraphFormat.Font.Size = 8;
            principalSubtitle.ParagraphFormat.Font.Bold = true;
            principalSubtitle.ParagraphFormat.SpaceBefore = 8f;
            principalSubtitle.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            principalSubtitle.ParagraphFormat.RightIndent = Size.TableCellPadding;

            // Style Principal DateTime
            var principalDateTime = doc.Styles.AddStyle(PrincipalDateTime, StyleNames.Normal);
            principalDateTime.ParagraphFormat.Font = new Font("Lato");
            principalDateTime.ParagraphFormat.Font.Size = 10;
            principalDateTime.ParagraphFormat.SpaceBefore = 7f;
            principalDateTime.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            principalDateTime.ParagraphFormat.RightIndent = Size.TableCellPadding;

            //Style Header Column Forms
            var columnHeader = doc.Styles.AddStyle(ColumnHeader, StyleNames.Normal);
            columnHeader.ParagraphFormat.Font = new Font("Lato-SemiBold");
            columnHeader.ParagraphFormat.Font.Size = 9;
            columnHeader.ParagraphFormat.Font.Bold = true;
            columnHeader.ParagraphFormat.Font.Color = Color.FromRgb(255, 255, 255);
            columnHeader.ParagraphFormat.SpaceBefore = 9f;
            columnHeader.ParagraphFormat.Shading.Color = _Zafiro; 
            columnHeader.ParagraphFormat.Borders.Distance = Size.TableCellPadding;

            //Style Content Column Forms
            var columnContent = doc.Styles.AddStyle(ColumnContent, StyleNames.Normal);
            columnContent.ParagraphFormat.Font = new Font("Lato-Regular");
            columnContent.ParagraphFormat.Font.Color = Color.FromRgb(0, 0, 0);
            columnContent.ParagraphFormat.Font.Size = 9;
            columnContent.ParagraphFormat.Shading.Color = Color.FromRgb(232, 232, 232);
            columnContent.ParagraphFormat.Borders.Distance = Size.TableCellPadding;

            //Style Header Column Forms
            var columnHeaderCenter = doc.Styles.AddStyle(ColumnHeaderCenter, StyleNames.Normal);
            columnHeaderCenter.ParagraphFormat.Font.Bold = true;
            columnHeaderCenter.ParagraphFormat.Font = new Font("Lato-SemiBold");
            columnHeaderCenter.ParagraphFormat.SpaceBefore = 9f;
            columnHeaderCenter.ParagraphFormat.Font.Color = Color.FromRgb(255, 255, 255);
            columnHeaderCenter.ParagraphFormat.Shading.Color = _Zafiro;
            columnHeaderCenter.ParagraphFormat.Borders.Distance = Size.TableCellPadding;
            columnHeaderCenter.ParagraphFormat.Alignment = ParagraphAlignment.Center;


            var secondColumnHeaderCenter = doc.Styles.AddStyle(SecondColumnHeaderCenter, StyleNames.Normal);
            secondColumnHeaderCenter.ParagraphFormat.Font = new Font("Lato-SemiBold");
            secondColumnHeaderCenter.ParagraphFormat.Font.Size = 9;
            secondColumnHeaderCenter.ParagraphFormat.Font.Bold = true;
            secondColumnHeaderCenter.ParagraphFormat.Font.Color = Color.FromRgb(255, 255, 255);
            secondColumnHeaderCenter.ParagraphFormat.Borders.Distance = SizeDistance;
            secondColumnHeaderCenter.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            secondColumnHeaderCenter.ParagraphFormat.Shading.Color = _Zafiro;

            //Style Content Column Forms
            var columnContentCenter = doc.Styles.AddStyle(ColumnContentCenter, StyleNames.Normal);
            columnContentCenter.ParagraphFormat.Font = new Font("Lato-Regular");
            columnContentCenter.ParagraphFormat.Font.Color = Color.FromRgb(0, 0, 0);
            columnContentCenter.ParagraphFormat.Font.Size = 9;
            columnContentCenter.ParagraphFormat.Shading.Color = Color.FromRgb(232, 232, 232);
            columnContentCenter.ParagraphFormat.Borders.Distance = Size.TableCellPadding ;
            columnContentCenter.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            //Style Title Paragraph
            var titleParagraph = doc.Styles.AddStyle(TitleParagraph, StyleNames.Normal);
            titleParagraph.ParagraphFormat.Font.Bold = true;
            titleParagraph.ParagraphFormat.SpaceBefore = 5;
            titleParagraph.ParagraphFormat.Font.Color = Color.FromRgb(0, 0, 0);

            //Style Paragraph Small
            var paragraphSmall = doc.Styles.AddStyle(ParagraphSmall, StyleNames.Normal);
            paragraphSmall.ParagraphFormat.Font = new Font("Lato");
            paragraphSmall.ParagraphFormat.Font.Size = 6;
            //paragraphSmall.ParagraphFormat.SpaceBefore = 6f;
            paragraphSmall.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            paragraphSmall.ParagraphFormat.RightIndent = Size.TableCellPadding;

            //Style Paragraph Small
            var paragraphSmallOnePage = doc.Styles.AddStyle(ParagraphSmallFirstPage, StyleNames.Normal);
            paragraphSmallOnePage.ParagraphFormat.Font = new Font("Lato");
            paragraphSmallOnePage.ParagraphFormat.Font.Size = 7;
            paragraphSmallOnePage.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            paragraphSmallOnePage.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            paragraphSmallOnePage.ParagraphFormat.RightIndent = Size.TableCellPadding;

            //Style Paragraph Small
            var textTakeOff = doc.Styles.AddStyle(TextTakeOff, StyleNames.Normal);
            textTakeOff.ParagraphFormat.Font = new Font("Lato");
            textTakeOff.ParagraphFormat.Font.Size = 8;
            textTakeOff.ParagraphFormat.SpaceBefore = 15f;
            textTakeOff.ParagraphFormat.SpaceAfter = 15f;
            textTakeOff.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            textTakeOff.ParagraphFormat.RightIndent = Size.TableCellPadding;

            //Coverages List
            var coveragesList = doc.Styles.AddStyle(CoveragesList, StyleNames.Normal);
            coveragesList.ParagraphFormat.Font = new Font("Lato");
            coveragesList.ParagraphFormat.Font.Size = 10;

            //Coverages List Text
            var coveragesListBasic = doc.Styles.AddStyle(CoveragesListBasic, StyleNames.Normal);
            coveragesListBasic.ParagraphFormat.Font = new Font("Lato");
            coveragesListBasic.ParagraphFormat.Font.Size = 10;
            coveragesListBasic.ParagraphFormat.SpaceAfter = 7f;

            var textProjection = doc.Styles.AddStyle(TextProjection, StyleNames.Normal);
            textProjection.ParagraphFormat.Font = new Font("Lato");
            textProjection.ParagraphFormat.Font.Size = 10;
            textProjection.ParagraphFormat.SpaceBefore = 10f;
            textProjection.ParagraphFormat.SpaceAfter = 5f;
            textProjection.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            textProjection.ParagraphFormat.RightIndent = Size.TableCellPadding;


            var titleParagraphNoBold = doc.Styles.AddStyle(TitleParagraphNoBold, StyleNames.Normal);
            titleParagraphNoBold.ParagraphFormat.SpaceBefore = 8f;
            titleParagraphNoBold.ParagraphFormat.Font.Color = Color.FromRgb(0, 0, 0);
            titleParagraphNoBold.ParagraphFormat.Borders.Distance = Size.TableCellPadding;

            var paragraphNormal = doc.Styles.AddStyle(ParagraphNormal, StyleNames.Normal);
            paragraphNormal.ParagraphFormat.Font = new Font("Lato Regular");
            paragraphNormal.ParagraphFormat.Font.Size = 10;
            paragraphNormal.ParagraphFormat.SpaceBefore = 10f;
            paragraphNormal.ParagraphFormat.SpaceAfter = 10f;
            paragraphNormal.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            paragraphNormal.ParagraphFormat.RightIndent = Size.TableCellPadding;
            paragraphNormal.ParagraphFormat.Alignment = ParagraphAlignment.Justify;


            var footerText = doc.Styles.AddStyle(FooterText, StyleNames.Normal);
            footerText.ParagraphFormat.Font = new Font("Lato");
            footerText.ParagraphFormat.Font.Size = 7;
            footerText.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            footerText.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            footerText.ParagraphFormat.RightIndent = Size.TableCellPadding;
          
            var signatureLeft = doc.Styles.AddStyle(SignatureLeft, StyleNames.Normal);
            signatureLeft.ParagraphFormat.Font = new Font("Lato");
            signatureLeft.ParagraphFormat.Font.Size = 10;   
            signatureLeft.ParagraphFormat.SpaceBefore = 10f;
            signatureLeft.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            signatureLeft.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            signatureLeft.ParagraphFormat.RightIndent = Size.TableCellPadding;


            var signatureRight = doc.Styles.AddStyle(SignatureRight, StyleNames.Normal);
            signatureRight.ParagraphFormat.Font = new Font("Lato");
            signatureRight.ParagraphFormat.Font.Size = 10;
            signatureRight.ParagraphFormat.SpaceBefore = 10f;
            signatureRight.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            signatureRight.ParagraphFormat.LeftIndent = Size.TableCellPadding;
            signatureRight.ParagraphFormat.RightIndent = Size.TableCellPadding;


            var signatureTextLeft = doc.Styles.AddStyle(SignatureTextLeft, StyleNames.Normal);
            signatureTextLeft.ParagraphFormat.Font = new Font("Lato");
            signatureTextLeft.ParagraphFormat.Font.Size = 7;
            signatureTextLeft.ParagraphFormat.SpaceBefore = 4f;
            signatureTextLeft.ParagraphFormat.SpaceAfter = 4f;
            signatureTextLeft.ParagraphFormat.LeftIndent = 29;
            signatureTextLeft.ParagraphFormat.RightIndent = Size.TableCellPadding;
            signatureTextLeft.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            var lineColor = doc.Styles.AddStyle(LineColor, StyleNames.Normal);
            lineColor.ParagraphFormat.Font.Color = _Zafiro;
            lineColor.ParagraphFormat.Font.Bold = true;


            var columnContentGray = doc.Styles.AddStyle(ColumnContentGray, StyleNames.Normal);
            columnContentGray.ParagraphFormat.Font = new Font("Lato Regular");
            columnContentGray.ParagraphFormat.Font.Color = Color.FromRgb(232, 232, 232);
            columnContentGray.ParagraphFormat.Font.Size = 10;
            columnContentGray.ParagraphFormat.Borders.Distance = Size.TableCellPadding;

            var columnContentWhite = doc.Styles.AddStyle(ColumnContentWhite, StyleNames.Normal);
            columnContentWhite.ParagraphFormat.Font = new Font("Lato Regular");
            columnContentWhite.ParagraphFormat.Font.Color = Colors.White;
            columnContentWhite.ParagraphFormat.Font.Size = 10;
            columnContentWhite.ParagraphFormat.Borders.Distance = Size.TableCellPadding;


            //Style Content Column Forms
            var finishRow = doc.Styles.AddStyle(FinishRow, StyleNames.Normal);
            finishRow.ParagraphFormat.Borders.Distance = SizeDistance;
            finishRow.ParagraphFormat.Font = new Font("Lato Regular");
            finishRow.ParagraphFormat.Font.Color = Color.FromRgb(0, 0, 0);
            finishRow.ParagraphFormat.Font.Size = 10;

            #endregion
        }
    }
}
