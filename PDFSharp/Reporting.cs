
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PDFSharp.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PDFSharp
{
    public class Reporting
    {
        public static readonly Color Shading = new Color(243, 243, 243);

        public void Export(string path)
        {
            ExportPdf(path, CreateReport());
        }

        private void ExportPdf(string path, Document report)
        {
            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = report;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(path);
        }

        private Document CreateReport()
        {
            var doc = new Document();

            //TODO
            //SE AGREGAN LOS ESCTILOS PERSONALIZADOS AL DOCUMENTO
            CustomStyles.Define(doc);

            //SE CREA SECCIÓN Y CONTENIDO
            doc.Add(CreateMainSection());

            return doc;
        }

        private Section CreateMainSection()
        {
            //UNA SECCIÓN PARA TODO EL DOCUMENTO, LUEGO SE ENCARGA DE SEPARARLO EN PAGINAS
            var section = new Section();

            SetUpPage(section);

            //AddHeaderAndFooter(section, data);

            AddContents(section);

            return section;
        }

        private void SetUpPage(Section section)
        {
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.LeftMargin = Size.LeftRightPageMargin;
            section.PageSetup.TopMargin = Size.TopBottomPageMargin;
            section.PageSetup.RightMargin = Size.LeftRightPageMargin;
            section.PageSetup.BottomMargin = Size.TopBottomPageMargin;

            section.PageSetup.HeaderDistance = Size.HeaderFooterMargin;
            section.PageSetup.FooterDistance = Size.HeaderFooterMargin;
        }

        private void AddHeaderAndFooter(Section section)
        {
            new HeaderAndFooter().Add(section);
        }

        private void AddContents(Section section)
        {
            //Titulo principal
            var _titleHeader = section.AddParagraph();

            _titleHeader.AddFormattedText("Cotización de producto Zafiro", CustomStyles.PrincipalTitle);

            //Subtitulo
            var _subTitleHeader = section.AddParagraph();

            _subTitleHeader.AddText("(Con indicación de la moneda contratada).");
            _subTitleHeader.Style = CustomStyles.PrincipalSubtitle;

            //Lugar y Fecha
            var _placeDate = section.AddParagraph();
            var placeDocument = "Buenos Aires, Argentina";
            var dateDocument = "01/04/2020";

            _placeDate.AddText("Lugar: " + placeDocument);

            AddCharRepeat(_placeDate, "_", 70 - placeDocument.Length, CustomStyles.ColumnContentWhite);

            _placeDate.AddText(" Fecha: " + dateDocument + ".");

            _placeDate.Style = CustomStyles.PrincipalDateTime;

            //Insured
            #region Insured Section
            AddTableInsured(section);
            #endregion

            #region Contractor Section
            AddTableContractor(section);
            #endregion

            #region Insurance Section
            AddTableInsurance(section);
            #endregion

            //Incremento
            var _incrementTitle = section.AddParagraph();

            AddTitleLine(section, "TIPO Y DURACIÓN DEL INCREMENTO:");

            var _incrementText = section.AddParagraph();
            _incrementText.AddText("Incrementos Automáticos: 3% DURANTE 5 AÑOS. La póliza incluye un beneficio de incremento automático de 3% del capital asegurado de la \n Cobertura Básica por Fallecimiento y de las Coberturas Adicionales, aplicable durante los 5 aniversarios siguientes a la fecha de inicio de vigencia de\n la póliza.Asimismo, las primas de las pólizas se ajustarán a este mismo porcentaje de incremento.");
            _incrementText.Style = CustomStyles.ParagraphSmallFirstPage;

            #region Variations

            //Variaciones de capital

            var _variationsTitle = section.AddParagraph();

            AddTitleLine(section, "PRIMAS, RETIROS Y VARIACIONES DE CAPITAL: ");

            AddTableVariations(section);
            #endregion

            #region CoverageAndCapital
            //Coberturas Y Capitales

            var dataCapitalCoverage = new CapitalCoverage();

            AddTitleLine(section, "COBERTURAS Y CAPITALES: ");

            var _CovBasicTitle = section.AddParagraph();
            _CovBasicTitle.AddFormattedText("Cobertura básica:", CustomStyles.CoveragesList);

            var _CovBasicText = section.AddParagraph();
            _CovBasicText.AddFormattedText(dataCapitalCoverage.bDeath ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovBasicText.AddText("_____________ Beneficio por Fallecimiento");
            _CovBasicText.Style = CustomStyles.CoveragesListBasic;

            var _CovAdditionalTitle = section.AddParagraph();
            _CovAdditionalTitle.AddFormattedText("Coberturas adicionales:", CustomStyles.CoveragesList);

            var _CovAdditionalTextOne = section.AddParagraph();
            _CovAdditionalTextOne.AddFormattedText(dataCapitalCoverage.bAccidentalDeath ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextOne.AddText("_____________ Indemnización Adicional por Muerte Accidental");
            _CovAdditionalTextOne.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextTwo = section.AddParagraph();
            _CovAdditionalTextTwo.AddFormattedText(dataCapitalCoverage.bAccidentalDeathLose ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextTwo.AddText("_____________ Indemnización Adicional por Muerte Accidental y Pérdidas Físicas Parciales por Accidente");
            _CovAdditionalTextTwo.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextThree = section.AddParagraph();
            _CovAdditionalTextThree.AddFormattedText(dataCapitalCoverage.bAccidentalDeathInvalidity ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextThree.AddText("_____________ Indemnización Adicional por Muerte Accidental e Invalidez Permanente por Accidente");
            _CovAdditionalTextThree.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextFour = section.AddParagraph();
            _CovAdditionalTextFour.AddFormattedText(dataCapitalCoverage.bInvalidityTotalExemptionMensual ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextFour.AddText("_____________ Invalidez total y permamente: Exención de la Deducción Mensual");
            _CovAdditionalTextFour.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextFive = section.AddParagraph();
            _CovAdditionalTextFive.AddFormattedText(dataCapitalCoverage.bInvalidityTotalExemptionPremium ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextFive.AddText("_____________ Invalidez total y permamente: Exención de Prima Planeada");
            _CovAdditionalTextFive.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextSix = section.AddParagraph();
            _CovAdditionalTextSix.AddFormattedText(dataCapitalCoverage.bAdditionalExemptionTotalPermanently ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextSix.AddText("_____________ Indemnización Adicional por Invalidez Total y Permanente");
            _CovAdditionalTextSix.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextSeven = section.AddParagraph();
            _CovAdditionalTextSeven.AddFormattedText(dataCapitalCoverage.bAdditionalExemptionDesease ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextSeven.AddText("_____________ Indemnización Adicional por Diagnóstico de Enfermedad Grave");
            _CovAdditionalTextSeven.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextEight = section.AddParagraph();
            _CovAdditionalTextEight.AddFormattedText(dataCapitalCoverage.bAdvanceDeseaseSerious ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextEight.AddText("_____________ Anticipo Del Beneficio por Fallecimiento de Enfermedad Grave");
            _CovAdditionalTextEight.Style = CustomStyles.CoveragesList;

            var _CovAdditionalTextNine = section.AddParagraph();
            _CovAdditionalTextNine.AddFormattedText(dataCapitalCoverage.bAdvanceDeseaseTerminal ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });
            _CovAdditionalTextNine.AddText("_____________ Anticipo Del Beneficio por Fallecimiento de Enfermedad Terminal");
            _CovAdditionalTextNine.Style = CustomStyles.CoveragesList;
            #endregion



            #region SecondPage
            AddTitleLine(section, "PROYECCIÓN: ");
            AddTableProjection(section);

            var _oneTextProjection = section.AddParagraph();
            _oneTextProjection.AddText("La presente es una ilustración, no un contrato. Los valores proyectados no están garantizados, solo representan una ilustración de proyecciones que se han constituido sobre los supuestos que se indican a continuación:");
            _oneTextProjection.Style = CustomStyles.TextProjection;

            var _twoTextProjection = section.AddParagraph();
            _twoTextProjection.AddText("Las primas se consideran al inicio de cada período. Los Valores de la Cuenta Individual, Valor de Rescate y Monto Asegurado Neto al Fallecimiento son ilustrativos al final de cada año.");
            _twoTextProjection.Style = CustomStyles.TextProjection;

            var _threeTextProjection = section.AddParagraph();
            _threeTextProjection.AddText("En caso de efectuar el pago de primas en Pesos, la cantidad de Dólares Estadounidenses con los que se realizó esta proyección podrá diferir del monto efectivamente ingresado en la Compañía.");
            _threeTextProjection.Style = CustomStyles.TextProjection;

            var _fourTextProjection = section.AddParagraph();
            _fourTextProjection.AddText("La tasa anual que se garantiza anualmente durante la vigencia de este plan es de 2,00% efectivo anual.");
            _fourTextProjection.Style = CustomStyles.TextProjection;

            section.AddPageBreak();

            #endregion

            #region ThirdyPage

            AddTitleLine(section, "QUITAS: ");


            AddTableTakeOffOne(section);

            var _textTakeOffOne = section.AddParagraph();
            _textTakeOffOne.AddText("Quita por rescate es el cargo que deducirá la Compañía de su Reserva Matemática cuando usted rescinda la póliza en forma anticipada (rescate) o transforme la póliza en un seguro saldado o prorrogado.");
            _textTakeOffOne.Style = CustomStyles.TextTakeOff;

            AddTableTakeOffTwo(section);

            var _textTakeOffTwo = section.AddParagraph();
            _textTakeOffTwo.AddText("El cargo por Rescisión es aquel que deducirá la Compañía de la Cuenta Individual o fondo ahorrado, cuando el tomador rescinda la póliza en forma anticipada (Rescate Total).");
            _textTakeOffTwo.Style = CustomStyles.TextTakeOff;

            section.AddPageBreak();
            #endregion

            #region FourPage
            AddTitleLine(section, "INICIO DE VIGENCIA DE LA COBERTURA: ");


            var _textCovValidity = section.AddParagraph();
            _textCovValidity.AddText("El seguro de Vida y las coberturas adicionales, solicitados en la presente proyección, entrarán en vigencia en la fecha indicada en las Condiciones Particulares de la póliza que emita la Compañía una vez aceptado el riesgo por la misma. SMG LIFE Seguros de Vida S.A.suspenderá la cobertura si la Prima Inicial no es pagada en la fecha indicada en las Condiciones Particulares de la póliza, a partir de las cero horas del día siguiente al del vencimiento del pago. SMG LIFE Seguros de Vida S.A.otorgará un plazo de un mes, sin cobertura, para el pago de la Prima Inicial, que se contará desde la fecha de su vencimiento para el pago.Vencido dicho plazo y no pagada la Prima Inicial, la póliza se considerará rescindida según lo dispuesto por el primer párrafo del artículo 134º de la Ley de Seguros.");
            _textCovValidity.Style = CustomStyles.TextProjection;

            AddTitleLine(section, "HABEAS DATA:    ");

            var _textHabeas = section.AddParagraph();
            _textHabeas.AddText("La suscripción de la presente, excepto opción personal por escrito en contrario, importará autorizar a SMG LIFE Seguros de Vida S.A.Seguros y / o a Swiss Medical S.A., a sus sociedades controladas, controlantes o vinculadas, a utilizar los datos no sensibles en futuras campañas de fidelización, los cuales serán resguardados según las Políticas Corporativas de Seguridad Informática y conforme a la Ley 25.326 de Protección de los Datos Personales.El titular tiene la posibilidad de ejercer los derechos de acceso, rectificación y supresión de los datos(artículos 6, 14, 16, 17 y concordantes, Ley 25.326 y / o la / s ");
            _textHabeas.Style = CustomStyles.ParagraphNormal;


            AddTitleLine(section, "UNIDAD DE INFORMACION FINANCIERA (UIF):");


            var _textUIF = section.AddParagraph();
            _textUIF.AddText("En cumplimiento de la Resolución N° 28/2018 y complementarias emitidas por la Unidad de Información Financiera (UIF), le comunicamos que, al momento de solicitar la contratación de un seguro, la Compañía se encuentra obligada a requerir información y / o documentación que nos permitan la correcta identificación y debido conocimiento de nuestros clientes, así como también solicitar respaldo del origen de los fondos con los cuales se adquieren los productos.La actualización de esta información y documentación podrá también requerirse a lo largo de la vida de la póliza y conforme a los niveles de riesgo que la Compañía determine. Adicionalmente y en cumplimiento de la Resolución Nº 11 / 2011 y N° 134 / 2018, N° 15 / 2019 y sus modificatorias y comple - mentarias, del mismo organismo, se requerirá la firma de la Declaración Jurada de identificación de clientes denominados “Personas Expuestas Políticamente(PEP)”.");
            _textUIF.Style = CustomStyles.ParagraphNormal;


            AddTableSignature(section);

            AddImageLogo(section);


            var _textFooterOne = section.AddParagraph();
            _textFooterOne.AddText("SMG LIFE Seguros de Vida S.A. - CUIT 30-68584340-0 - Arenales 1826 3º Piso - (C1124AAB) - CABA – Argentina \n Tel.: (011) 4814-9900 / Fax: (011) 4814-9973 - Departamento de atención al Cliente: 0810-222-7645 - www.swissmedicalseguros.com");
            _textFooterOne.Style = CustomStyles.FooterText;

            #endregion
        }

        private void AddTableInsured(Section section)
        {

            var dataInsured = new Insured();

            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;

            _table.AddColumn(15);
            _table.AddColumn(Size.GetWidth(section) - 30);
            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Cells[0].MergeRight = 2;

            var _rowParHeader = _rowHeader.Cells[0].AddParagraph();
            _rowParHeader.AddText("DATOS DEL PROPUESTO ASEGURADO:");
            _rowParHeader.Style = CustomStyles.ColumnHeader;


            var _rowOne = _table.AddRow();
            _rowOne.TopPadding = 0;
            _rowOne.BottomPadding = 0;
            _rowOne.Cells[0].MergeRight = 2;

            var _rowParOne = _rowOne.Cells[0].AddParagraph();

            _rowParOne.AddFormattedText("Nombre y apellido: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsured.sFullName);

            AddCharRepeat(_rowParOne, "_", 50 - dataInsured.sFullName.Length, CustomStyles.ColumnContentGray);


            _rowParOne.AddFormattedText("DNI: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsured.sDocument);
            
            _rowParOne.Style = CustomStyles.ColumnContent;

            var _rowTwo = _table.AddRow();
            _rowTwo.TopPadding = 0;
            _rowTwo.BottomPadding = 0;
            _rowTwo.Cells[0].MergeRight = 2;

            var _rowParTwo = _rowTwo.Cells[0].AddParagraph();

            _rowParTwo.AddFormattedText("Fecha de Nacimiento: ", TextFormat.Bold);
            _rowParTwo.AddText(dataInsured.sDate);
            

            _rowParTwo.AddFormattedText("Género: ", TextFormat.Bold);


            _rowParTwo.AddFormattedText(dataInsured.bFemale ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" F  ");

            _rowParTwo.AddFormattedText(dataInsured.bMale ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" M  ");
            _rowParTwo.AddFormattedText("¿Fumador ?:  ", TextFormat.Bold);
            //¿Fumador ?: 

            _rowParTwo.AddFormattedText(dataInsured.bSmokerYes ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" SÍ  ");

            _rowParTwo.AddFormattedText(dataInsured.bSmokerNo ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" NO.");

            _rowParTwo.Style = CustomStyles.ColumnContent;

            //FILA FINAL SE REDONDEAS
            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);

        }
        private void AddTableContractor(Section section)
        {
            var dataContractor = new Contractor();
            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;

            _table.AddColumn(15);
            _table.AddColumn(Size.GetWidth(section) - 30);
            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Cells[0].MergeRight = 2;

            var _rowParHeader = _rowHeader.Cells[0].AddParagraph();
            _rowParHeader.AddText("DATOS DEL PROPUESTO CONTRATANTE:");
            _rowParHeader.Style = CustomStyles.ColumnHeader;

            var _rowOne = _table.AddRow();
            _rowOne.TopPadding = 0;
            _rowOne.BottomPadding = 0;
            _rowOne.Cells[0].MergeRight = 2;

            var _rowParOne = _rowOne.Cells[0].AddParagraph();


            _rowParOne.AddFormattedText("Nombre y apellido: ", TextFormat.Bold);
            _rowParOne.AddText(dataContractor.sFullName);

            AddCharRepeat(_rowParOne, "_", 50 - dataContractor.sFullName.Length, CustomStyles.ColumnContentGray);

            _rowParOne.AddFormattedText("DNI: ", TextFormat.Bold);
            _rowParOne.AddText(dataContractor.sDocument);

            _rowParOne.Style = CustomStyles.ColumnContent;

            var _rowTwo = _table.AddRow();
            _rowTwo.TopPadding = 0;
            _rowTwo.BottomPadding = 0;
            _rowTwo.Cells[0].MergeRight = 2;

            var _rowParTwo = _rowTwo.Cells[0].AddParagraph();

            _rowParTwo.AddFormattedText("Fecha de Nacimiento: ", TextFormat.Bold);
            _rowParTwo.AddText(dataContractor.sDate);

            _rowParTwo.AddFormattedText("Género: ", TextFormat.Bold);

            _rowParTwo.AddFormattedText(dataContractor.bFemale ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" F  ");

            _rowParTwo.AddFormattedText(dataContractor.bMale ? "\u00fe" : "\u00A8", new Font("Wingdings") { Size = 14 });

            _rowParTwo.AddText(" M ");
                
            _rowParTwo.Style = CustomStyles.ColumnContent;

            var _rowThree = _table.AddRow();
            _rowThree.TopPadding = 0;
            _rowThree.BottomPadding = 0;
            _rowThree.Cells[0].MergeRight = 2;

            var _rowParThree = _rowThree.Cells[0].AddParagraph();

            _rowParThree.AddFormattedText("Razón Social: ", TextFormat.Bold);
            _rowParThree.AddText(dataContractor.sSocialReason);
            
            AddCharRepeat(_rowParThree, "_", 53 - dataContractor.sSocialReason.Length, CustomStyles.ColumnContentGray);

            _rowParThree.AddFormattedText("CUIT: ", TextFormat.Bold);
            _rowParThree.AddText(dataContractor.sCuit);

            _rowParThree.Style = CustomStyles.ColumnContent;

            //FILA FINAL SE REDONDEAS
            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);
        }
        private void AddTableInsurance(Section section)
        {
            var _table = section.AddTable();
            var dataInsurance = new Insurance();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;

            _table.AddColumn(15);
            _table.AddColumn(Size.GetWidth(section) - 30);
            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Cells[0].MergeRight = 2;

            var _rowParHeader = _rowHeader.Cells[0].AddParagraph();
            _rowParHeader.AddText("DATOS GENERALES DEL SEGURO:");
            _rowParHeader.Style = CustomStyles.ColumnHeader;

            var _rowOne = _table.AddRow();
            _rowOne.TopPadding = 0;
            _rowOne.BottomPadding = 0;
            _rowOne.Cells[0].MergeRight = 2;

            var _rowParOne = _rowOne.Cells[0].AddParagraph();

            _rowParOne.AddFormattedText("Fecha de vigencia: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsurance.sDateValidity);

            _rowParOne.AddFormattedText("Edad fin de vigencia: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsurance.sAgeEndVigency);

            AddCharRepeat(_rowParOne, "_", 3 - dataInsurance.sAgeEndVigency.Length, CustomStyles.ColumnContentGray);

            _rowParOne.AddFormattedText("Edad Fin de Pago: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsurance.sEndPayment);
            AddCharRepeat(_rowParOne, "_", 3 - dataInsurance.sEndPayment.Length, CustomStyles.ColumnContentGray);

            _rowParOne.AddFormattedText(" Derecho de Emisión: ", TextFormat.Bold);
            _rowParOne.AddText(dataInsurance.sRightIssue);


            _rowParOne.Style = CustomStyles.ColumnContent;

            var _rowTwo = _table.AddRow();
            _rowTwo.TopPadding = 0;
            _rowTwo.BottomPadding = 0;
            _rowTwo.Cells[0].MergeRight = 2;

            var _rowParTwo = _rowTwo.Cells[0].AddParagraph();


            _rowParTwo.AddFormattedText("Prima periódica a pagar: ", TextFormat.Bold);
            _rowParTwo.AddText(dataInsurance.sPeriodicPremium);
            AddCharRepeat(_rowParTwo, "_", 8 - dataInsurance.sPeriodicPremium.Length, CustomStyles.ColumnContentGray);
            
            _rowParTwo.AddFormattedText("Duración del seguro: ", TextFormat.Bold);
            _rowParTwo.AddText(dataInsurance.sDurationInsurance);
            AddCharRepeat(_rowParTwo, "_", 9 - dataInsurance.sDurationInsurance.Length, CustomStyles.ColumnContentGray);

            _rowParTwo.AddFormattedText("Frecuencia de Pago: ", TextFormat.Bold);
            _rowParTwo.AddText(dataInsurance.sFrecuencyPayment);
            AddCharRepeat(_rowParTwo, "_", 8 - dataInsurance.sFrecuencyPayment.Length, CustomStyles.ColumnContentGray);

            _rowParTwo.Style = CustomStyles.ColumnContent;

            var _rowThree = _table.AddRow();

            _rowThree.TopPadding = 0;
            _rowThree.BottomPadding = 0;
            _rowThree.Cells[0].MergeRight = 2;

            var _rowParThree = _rowThree.Cells[0].AddParagraph();

            _rowParThree.AddFormattedText("Modalidad: ", TextFormat.Bold);
            _rowParThree.AddText(dataInsurance.sModality);
            AddCharRepeat(_rowParThree, "_", 14 - dataInsurance.sModality.Length, CustomStyles.ColumnContentGray);
            
            _rowParThree.AddFormattedText("Provincia Fiscal: ", TextFormat.Bold);
            _rowParThree.AddText(dataInsurance.sProvinceFiscal);
            AddCharRepeat(_rowParThree, "_", 21 - dataInsurance.sProvinceFiscal.Length, CustomStyles.ColumnContentGray);
            
            _rowParThree.AddFormattedText("Opción de Indemnización: ", TextFormat.Bold);
            _rowParThree.AddText(dataInsurance.sOptionCompensation);

            _rowParThree.Style = CustomStyles.ColumnContent;

            //FILA FINAL SE REDONDEAS
            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);
        }
        private void AddTableVariations(Section section)
        {
            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;

            var width = (Size.GetWidth(section) - 30) / 5;

            _table.AddColumn(15);

            _table.AddColumn(width);
            _table.AddColumn(width);
            _table.AddColumn(width);
            _table.AddColumn(width);
            _table.AddColumn(width);

            _table.AddColumn(15);

            //FILA CABECERA
            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Shading.Color = CustomStyles._Zafiro;

            _rowHeader.Cells[0].Borders.Left.Color = Colors.White;
            _rowHeader.Cells[1].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[2].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[3].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[4].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[6].Borders.Right.Color = Colors.White;


            //COLUMNAS
            _rowHeader.Cells[0].MergeRight = 1;
            var columnOnePar = _rowHeader.Cells[0].AddParagraph();
            columnOnePar.AddText("CONCEPTO");
            columnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;
            _rowHeader.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            var columnTwoPar = _rowHeader.Cells[2].AddParagraph();
            columnTwoPar.AddText("PRIMA NETA");
            columnTwoPar.Style = CustomStyles.SecondColumnHeaderCenter;
            _rowHeader.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            var columnThreePar = _rowHeader.Cells[3].AddParagraph();
            columnThreePar.AddText("SELLADOS E IMPUESTOS");
            columnThreePar.Style = CustomStyles.SecondColumnHeaderCenter;
            _rowHeader.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            var columnFourPar = _rowHeader.Cells[4].AddParagraph();
            columnFourPar.AddText("CARGOS DE COBRANZAS");
            columnFourPar.Style = CustomStyles.SecondColumnHeaderCenter;
            _rowHeader.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            _rowHeader.Cells[5].MergeRight = 1;
            var columnFivePar = _rowHeader.Cells[5].AddParagraph();
            columnFivePar.AddText("IMPORTE A PAGAR/ CAPITAL ASEGURADO");
            columnFivePar.Style = CustomStyles.SecondColumnHeaderCenter;
            _rowHeader.Cells[5].VerticalAlignment = VerticalAlignment.Center;


            var colNames = new string[] { "Prima Inicial", "Prima Planeada" };



            for (int y = 0; y < colNames.Length; y++)
            {

                var _rowDetail = _table.AddRow();
                _rowDetail.TopPadding = 0;
                _rowDetail.BottomPadding = 0;

                //FILAS
                _rowDetail.Cells[0].MergeRight = 1;
                var cellOne = _rowDetail.Cells[0].AddParagraph();
                cellOne.AddText(colNames[y]);
                cellOne.Style = CustomStyles.ColumnContentCenter;

                var cellTwo = _rowDetail.Cells[2].AddParagraph();
                cellTwo.AddText("$99.99");
                cellTwo.Style = CustomStyles.ColumnContentCenter;

                var cellThree = _rowDetail.Cells[3].AddParagraph();
                cellThree.AddText("$99.99");
                cellThree.Style = CustomStyles.ColumnContentCenter;

                var cellFour = _rowDetail.Cells[4].AddParagraph();
                cellFour.AddText("$99.99");
                cellFour.Style = CustomStyles.ColumnContentCenter;
                //_rowDetail.Cells[4].Borders.Right.Color = Colors.Black;

                _rowDetail.Cells[5].MergeRight = 1;
                var cellFive = _rowDetail.Cells[5].AddParagraph();
                cellFive.AddText("$99.99");
                cellFive.Style = CustomStyles.ColumnContentCenter;

            }

            for (int z = 1; z < _table.Rows.Count - 1; z++)
            {
                _table.Rows[z].Borders.Bottom.Color = Colors.Black;

                for (int x = 0; x < _table.Columns.Count - 1; x++)
                {
                    if (x == 1)
                        x = 2;
                    _table.Rows[z].Cells[x].Borders.Right.Color = Colors.Black;

                }

            }

            for (int y = 0; y < _table.Columns.Count - 1; y++)
            {
                if (y == 1)
                    y = 2;
                _table.Rows[_table.Rows.Count - 1].Cells[y].Borders.Right.Color = Colors.Black;

            }

            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[2].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[3].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[4].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[5].Shading.Color = Color.FromRgb(232, 232, 232);

            _rowFinish.Cells[1].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[2].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[3].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[4].Borders.Right.Color = Colors.Black;

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);


            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);


        }
        private void AddTableProjection(Section section)
        {

            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;


            var width = (Size.GetWidth(section) - 30) / 9;

            _table.AddColumn(15);

            _table.AddColumn(34.80);
            _table.AddColumn(44.80);
            _table.AddColumn(64.80);
            _table.AddColumn(64.80);
            _table.AddColumn(width);
            _table.AddColumn(69.80);
            _table.AddColumn(64.80);
            _table.AddColumn(width);
            _table.AddColumn(width);

            _table.AddColumn(15);

            //FILA CABECERA
            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Shading.Color = CustomStyles._Zafiro;

            var _secondHeader = _table.AddRow();
            _secondHeader.TopPadding = 0;
            _secondHeader.BottomPadding = 0;
            _secondHeader.Shading.Color = CustomStyles._Zafiro;


            _rowHeader.Cells[0].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[2].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[3].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[4].Borders.Right.Color = Colors.White;
            _rowHeader.Cells[4].Borders.Bottom.Color = Colors.White;
            _rowHeader.Cells[7].Borders.Bottom.Color = Colors.White;
            _rowHeader.Cells[4].Borders.Right.Color = Colors.White;

            _secondHeader.Cells[4].Borders.Right.Color = Colors.White;
            _secondHeader.Cells[5].Borders.Right.Color = Colors.White;
            _secondHeader.Cells[6].Borders.Right.Color = Colors.White;
            _secondHeader.Cells[7].Borders.Right.Color = Colors.White;
            _secondHeader.Cells[8].Borders.Right.Color = Colors.White;


            //COLUMNAS
            _rowHeader.Cells[0].MergeRight = 1;
            _rowHeader.Cells[0].MergeDown = 1;
            var columnOnePar = _rowHeader.Cells[0].AddParagraph();
            columnOnePar.AddText("AÑO PÓLIZA");
            _rowHeader.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            columnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;


            _rowHeader.Cells[2].MergeDown = 1;
            var columnTwoPar = _rowHeader.Cells[2].AddParagraph();
            columnTwoPar.AddText("EDAD");
            _rowHeader.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            columnTwoPar.Style = CustomStyles.SecondColumnHeaderCenter;

            _rowHeader.Cells[3].MergeDown = 1;
            var columnThreePar = _rowHeader.Cells[3].AddParagraph();
            columnThreePar.AddText("PRIMAS PLANEADAS ANUALES");
            _rowHeader.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            columnThreePar.Style = CustomStyles.SecondColumnHeaderCenter;

            _rowHeader.Cells[4].MergeRight = 2;
            var columnFourPar = _rowHeader.Cells[4].AddParagraph();
            columnFourPar.AddText("VALORES PROYECTADOS AL X,00 %");
            columnFourPar.Style = CustomStyles.SecondColumnHeaderCenter;

            _rowHeader.Cells[7].MergeRight = 3;
            var columnFivePar = _rowHeader.Cells[7].AddParagraph();
            columnFivePar.AddText("VALORES PROYECTADOS AL X,00 %");
            columnFivePar.Style = CustomStyles.SecondColumnHeaderCenter;

            var secondColumnFourPar = _secondHeader.Cells[4].AddParagraph();
            secondColumnFourPar.AddText("VALOR DE CUENTA INDIVIDUAL");
            secondColumnFourPar.Style = CustomStyles.SecondColumnHeaderCenter;

            var secondColumnFivePar = _secondHeader.Cells[5].AddParagraph();
            secondColumnFivePar.AddText("VALOR DE RESCATE");
            secondColumnFivePar.Style = CustomStyles.SecondColumnHeaderCenter;

            var secondColumnSixPar = _secondHeader.Cells[6].AddParagraph();
            _secondHeader.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            secondColumnSixPar.AddText("MONTO ASEGURADO");
            secondColumnSixPar.Style = CustomStyles.SecondColumnHeaderCenter;

            var secondColumnSevenPar = _secondHeader.Cells[7].AddParagraph();
            secondColumnSevenPar.AddText("VALOR DE CUENTA INDIVIDUAL");
            secondColumnSevenPar.Style = CustomStyles.SecondColumnHeaderCenter;

            var secondColumnEightPar = _secondHeader.Cells[8].AddParagraph();
            secondColumnEightPar.AddText("VALOR DE RESCATE");
            secondColumnEightPar.Style = CustomStyles.SecondColumnHeaderCenter;

            _secondHeader.Cells[9].MergeRight = 1;
            var secondColumnNinePar = _secondHeader.Cells[9].AddParagraph();
            _secondHeader.Cells[9].VerticalAlignment = VerticalAlignment.Center;
            secondColumnNinePar.AddText("MONTO ASEGURADO");
            secondColumnNinePar.Style = CustomStyles.SecondColumnHeaderCenter;

            var years = 23;

            //20 registros pra proyección
            for (int i = 1; i < 21; i++)
            {
                years++;
                var _rowDetail = _table.AddRow();
                _rowDetail.TopPadding = 0;
                _rowDetail.BottomPadding = 0;

                //FILAS
                _rowDetail.Cells[0].MergeRight = 1;
                var cellOne = _rowDetail.Cells[0].AddParagraph();
                cellOne.AddText(i.ToString());
                cellOne.Style = CustomStyles.ColumnContentCenter;


                var cellTwo = _rowDetail.Cells[2].AddParagraph();
                cellTwo.AddText(years.ToString());
                cellTwo.Style = CustomStyles.ColumnContentCenter;

                var cellThree = _rowDetail.Cells[3].AddParagraph();
                cellThree.AddText("$99.99");
                cellThree.Style = CustomStyles.ColumnContentCenter;


                var cellFour = _rowDetail.Cells[4].AddParagraph();
                cellFour.AddText("$99.99");
                cellFour.Style = CustomStyles.ColumnContentCenter;

                var cellFive = _rowDetail.Cells[5].AddParagraph();
                cellFive.AddText("$ 99.99");
                cellFive.Style = CustomStyles.ColumnContentCenter;

                var cellSix = _rowDetail.Cells[6].AddParagraph();
                cellSix.AddText("$99.99");
                cellSix.Style = CustomStyles.ColumnContentCenter;

                var cellSeven = _rowDetail.Cells[7].AddParagraph();
                cellSeven.AddText("$99.99");
                cellSeven.Style = CustomStyles.ColumnContentCenter;

                var cellEight = _rowDetail.Cells[8].AddParagraph();
                cellEight.AddText("$99.99");
                cellEight.Style = CustomStyles.ColumnContentCenter;

                _rowDetail.Cells[9].MergeRight = 1;
                var cellNine = _rowDetail.Cells[9].AddParagraph();
                cellNine.AddText("$99.99");
                cellNine.Style = CustomStyles.ColumnContentCenter;


            }

            for (int z = 2; z < _table.Rows.Count - 1; z++)
            {
                _table.Rows[z].Borders.Bottom.Color = Colors.Black;

                for (int x = 0; x < _table.Columns.Count - 1; x++)
                {
                    if (x == 1)
                        x = 2;
                    _table.Rows[z].Cells[x].Borders.Right.Color = Colors.Black;

                }

            }

            for (int y = 0; y < _table.Columns.Count - 1; y++)
            {
                if (y == 1)
                    y = 2;
                _table.Rows[_table.Rows.Count - 1].Cells[y].Borders.Right.Color = Colors.Black;

            }

            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[2].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[3].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[4].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[5].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[6].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[7].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[8].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[9].Shading.Color = Color.FromRgb(232, 232, 232);


            _rowFinish.Cells[1].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[2].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[3].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[4].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[5].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[6].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[7].Borders.Right.Color = Colors.Black;
            _rowFinish.Cells[8].Borders.Right.Color = Colors.Black;

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);


        }
        private void AddTableTakeOffOne(Section section)
        {

            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;


            var width = (Size.GetWidth(section) - 30) / 2;


            _table.AddColumn(15);

            _table.AddColumn(width);
            _table.AddColumn(width);

            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Shading.Color = CustomStyles._Zafiro;

            var _secondHeader = _table.AddRow();
            _secondHeader.TopPadding = 0;
            _secondHeader.BottomPadding = 0;
            _secondHeader.Shading.Color = CustomStyles._Zafiro;
            _rowHeader.Cells[0].Borders.Bottom.Color = Colors.White;
            _secondHeader.Cells[0].Borders.Right.Color = Colors.White;

            _rowHeader.Cells[0].Borders.Bottom.Color = Colors.White;

            _rowHeader.Cells[0].MergeRight = 2;
            var columnOnePar = _rowHeader.Cells[0].AddParagraph();
            columnOnePar.AddText("FORMULARIO DE CONFORMIDAD CON LAS QUITAS POR RESCATE");
            columnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;

            _secondHeader.Cells[0].MergeRight = 1;
            var secondColumnOnePar = _secondHeader.Cells[0].AddParagraph();
            secondColumnOnePar.AddText("AÑO DE VIGENCIA");
            secondColumnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;

            _secondHeader.Cells[2].MergeRight = 1;
            var secondColumnTwoPar = _secondHeader.Cells[2].AddParagraph();
            secondColumnTwoPar.AddText("QUITA APLICABLE SOBRE LA RESERVA MATEMÁTICA");
            secondColumnTwoPar.Style = CustomStyles.SecondColumnHeaderCenter;


            for (int i = 1; i < 14; i++)
            {
                var text = i.ToString();

                if (i == 13)
                {
                    text = i.ToString() + " en adelante";
                }

                var _rowDetail = _table.AddRow();
                _rowDetail.TopPadding = 0;
                _rowDetail.BottomPadding = 0;

                //FILAS
                _rowDetail.Cells[0].MergeRight = 1;
                var cellOne = _rowDetail.Cells[0].AddParagraph();
                cellOne.AddText(text);
                cellOne.Style = CustomStyles.ColumnContentCenter;
                _rowDetail.Cells[0].Borders.Right.Color = Colors.Black;

                _rowDetail.Cells[2].MergeRight = 1;
                var cellTwo = _rowDetail.Cells[2].AddParagraph();
                cellTwo.AddText("XXXXXX");
                cellTwo.Style = CustomStyles.ColumnContentCenter;
            }

            for (int z = 2; z < _table.Rows.Count - 1; z++)
            {
                _table.Rows[z].Borders.Bottom.Color = Colors.Black;

                for (int x = 0; x < _table.Columns.Count - 1; x++)
                {
                    if (x == 1)
                        x = 2;
                    _table.Rows[z].Cells[x].Borders.Right.Color = Colors.Black;

                }

            }

            for (int y = 0; y < _table.Columns.Count - 1; y++)
            {
                if (y == 1)
                    y = 2;
                _table.Rows[_table.Rows.Count - 1].Cells[y].Borders.Right.Color = Colors.Black;

            }

            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[2].Shading.Color = Color.FromRgb(232, 232, 232);

            _rowFinish.Cells[1].Borders.Right.Color = Colors.Black;

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);

        }
        private void AddTableTakeOffTwo(Section section)
        {
            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;


            var width = (Size.GetWidth(section) - 30) / 2;

            _table.AddColumn(15);

            _table.AddColumn(width);
            _table.AddColumn(width);

            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Shading.Color = CustomStyles._Zafiro;

            var _secondHeader = _table.AddRow();
            _secondHeader.TopPadding = 0;
            _secondHeader.BottomPadding = 0;
            _secondHeader.Shading.Color = CustomStyles._Zafiro;

            _rowHeader.Cells[0].Borders.Bottom.Color = Colors.White;
            _secondHeader.Cells[0].Borders.Right.Color = Colors.White;

            _rowHeader.Cells[0].MergeRight = 2;
            var columnOnePar = _rowHeader.Cells[0].AddParagraph();
            columnOnePar.AddText("FORMULARIO DE CONFORMIDAD CON LAS QUITAS POR RESCATE");
            columnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;

            _secondHeader.Cells[0].MergeRight = 1;
            var secondColumnOnePar = _secondHeader.Cells[0].AddParagraph();
            secondColumnOnePar.AddText("AÑO DE VIGENCIA");
            secondColumnOnePar.Style = CustomStyles.SecondColumnHeaderCenter;

            _secondHeader.Cells[2].MergeRight = 1;
            var secondColumnTwoPar = _secondHeader.Cells[2].AddParagraph();
            secondColumnTwoPar.AddText("QUITA APLICABLE SOBRE LA RESERVA MATEMÁTICA");
            secondColumnTwoPar.Style = CustomStyles.SecondColumnHeaderCenter;


            for (int i = 1; i < 10; i++)
            {

                var text = i.ToString();

                if (i == 9)
                {
                    text = i.ToString() + " en adelante";
                }

                var _rowDetail = _table.AddRow();
                _rowDetail.TopPadding = 0;
                _rowDetail.BottomPadding = 0;

                //FILAS
                _rowDetail.Cells[0].MergeRight = 1;
                var cellOne = _rowDetail.Cells[0].AddParagraph();
                cellOne.AddText(text);
                cellOne.Style = CustomStyles.ColumnContentCenter;

                _rowDetail.Cells[2].MergeRight = 1;
                var cellTwo = _rowDetail.Cells[2].AddParagraph();
                cellTwo.AddText("XXXXXX");
                cellTwo.Style = CustomStyles.ColumnContentCenter;
            }

            for (int z = 2; z < _table.Rows.Count - 1; z++)
            {
                _table.Rows[z].Borders.Bottom.Color = Colors.Black;

                for (int x = 0; x < _table.Columns.Count - 1; x++)
                {
                    if (x == 1)
                        x = 2;
                    _table.Rows[z].Cells[x].Borders.Right.Color = Colors.Black;

                }

            }

            for (int y = 0; y < _table.Columns.Count - 1; y++)
            {
                if (y == 1)
                    y = 2;
                _table.Rows[_table.Rows.Count - 1].Cells[y].Borders.Right.Color = Colors.Black;

            }



            var _rowFinish = _table.AddRow();
            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Cells[1].Shading.Color = Color.FromRgb(232, 232, 232);
            _rowFinish.Cells[2].Shading.Color = Color.FromRgb(232, 232, 232);

            _rowFinish.Cells[1].Borders.Right.Color = Colors.Black;

            _table.Rows[_table.Rows.Count - 1].Cells[0].RoundedCorner = RoundedCorner.BottomLeft;
            _table.Rows[_table.Rows.Count - 1].Cells[0].Shading.Color = Color.FromRgb(232, 232, 232);

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Shading.Color = Color.FromRgb(232, 232, 232);
        }
        public void AddTableSignature(Section section)
        {
            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;


            var width = (Size.GetWidth(section) - 30) / 2;


            _table.AddColumn(width);
            _table.AddColumn(width);


            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 150;
            _rowHeader.BottomPadding = 0;

            var _textColumnLeftRow = _table.AddRow();
            _textColumnLeftRow.BottomPadding = 80;


            var columnOnePar = _rowHeader.Cells[0].AddParagraph();
            columnOnePar.AddText("--------------------------------------------------- \n Firma del Propuesto Tomador");
            columnOnePar.Style = CustomStyles.SignatureLeft;

            var _textLeftSignature = _textColumnLeftRow.Cells[0].AddParagraph();
            _textLeftSignature.AddText("Este reporte es válido únicamente por un período de 30 días. \n SMG LIFE Seguros de Vida S.A");
            _textLeftSignature.Style = CustomStyles.SignatureTextLeft;

            var columnTwoPar = _rowHeader.Cells[1].AddParagraph();
            columnTwoPar.AddText("--------------------------------------------------- \n Aclaración del Propuesto Tomador");
            columnTwoPar.Style = CustomStyles.SignatureRight;



        }
        public void AddImageLogo(Section section)
        {
            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.TopPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.BottomPadding = Size.TableCellPadding;


            var width = (Size.GetWidth(section) - 30);


            _table.AddColumn(width);


            var _rowHeader = _table.AddRow();
            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 10;


            var columnOnePar = _rowHeader.Cells[0].AddParagraph().AddImage(@"C:\Users\rahum\Downloads\PDFSharp\PDFSharp\logo_smg.png");

            columnOnePar.Width = 150;
            columnOnePar.Height = 30;

            _rowHeader.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            _rowHeader.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        }
        private void AddCharRepeat(Paragraph paragraph, string character, int count, string styleName = "")
        {
            paragraph.AddFormattedText(string.Concat(Enumerable.Repeat(character, count)), styleName);
        }
        private void AddTitleLine(Section section, string title)
        {

            var _table = section.AddTable();

            _table.Rows.LeftIndent = 0;
            _table.LeftPadding = Size.TableCellPadding;
            _table.RightPadding = Size.TableCellPadding;
            _table.AddColumn(title.Length * 6.5);
            _table.AddColumn(15);

            var _rowHeader = _table.AddRow();

            _rowHeader.TopPadding = 0;
            _rowHeader.BottomPadding = 0;
            _rowHeader.Borders.DistanceFromBottom = 0;

            var _rowParHeader = _rowHeader.Cells[0].AddParagraph();

            _rowParHeader.AddText(title);
            _rowParHeader.Style = CustomStyles.TitleParagraph;

            var _rowFinish = _table.AddRow();

            _rowFinish.TopPadding = 0;
            _rowFinish.BottomPadding = 0;
            _rowFinish.Borders.DistanceFromTop = 0;
            _rowFinish.Borders.DistanceFromBottom = 15f;
            _rowFinish.Cells[0].Borders.Bottom.Color = CustomStyles._Zafiro;

            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].RoundedCorner = RoundedCorner.BottomRight;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Borders.Bottom.Color = CustomStyles._Zafiro;
            _table.Rows[_table.Rows.Count - 1].Cells[_table.Columns.Count - 1].Borders.Right.Color = CustomStyles._Zafiro;

            _table.AddRow();

        }
    }
}
