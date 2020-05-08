namespace PDFSharp
{
    public class Insured
    {

        public string sFullName { get { return "José Pedro Peréz"; } }
        public string sDocument { get { return "43152929"; } }
        public string sDate { get { return "17/02/1997 "; } }
        public bool bMale { get { return true; } }
        public bool bFemale { get { return false; } }

        public bool bSmokerYes { get { return true; } }
        public bool bSmokerNo { get { return false; } }
    }


    public class Contractor
    {
        public string sFullName { get { return "Manuel Gomez Rivadavia"; } }
        public string sDocument { get { return "10982021"; } }
        public string sDate { get { return "03/02/1942 "; } }
        public bool bMale { get { return true; } }
        public bool bFemale { get { return false; } }
        public string sSocialReason { get { return "Grupo Asociados"; } }
        public string sCuit { get { return "30-71031609-7"; } }
    };

    public class Insurance
    {
        public string sDateValidity { get { return "20/02/2020 "; } }
        public string sAgeEndVigency { get { return "10"; } }
        public string sEndPayment { get { return "10"; } }
        public string sRightIssue { get { return "XXXXXX"; } }
        public string sPeriodicPremium { get { return "$100.00"; } }
        public string sDurationInsurance { get { return "10 años"; } }
        public string sFrecuencyPayment { get { return "Mensual"; } }
        public string sModality { get { return "XXXXXX"; } }

        public string sProvinceFiscal { get { return "Capital Federal"; } }
        public string sOptionCompensation { get { return "NiveladaEjemplo"; } }


    };

    public class CapitalCoverage
    {
        public bool bDeath { get { return true; } }
        public bool bAccidentalDeath { get { return false; } }

        public bool bAccidentalDeathLose { get { return false; } }
        public bool bAccidentalDeathInvalidity { get { return false; } }
        public bool bInvalidityTotalExemptionMensual { get { return true; } }
        public bool bInvalidityTotalExemptionPremium { get { return false; } }
        public bool bAdditionalExemptionTotalPermanently { get { return false; } }

        public bool bAdditionalExemptionDesease{ get { return true; } }

        public bool bAdvanceDeseaseSerious{ get { return false; } }

        public bool bAdvanceDeseaseTerminal { get { return true; } }
    }
};

