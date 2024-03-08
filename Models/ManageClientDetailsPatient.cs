using System;
using System.Collections.Generic;

namespace VMMC.Models;

public partial class ManageClientDetailsPatient
{
    public int Patientid { get; set; }

    public int? Identificationid { get; set; }

    public string? Identitydocument { get; set; }

    public string? Saidnumber { get; set; }

    public string? Passportnumber { get; set; }

    public string? Assylumnumber { get; set; }

    public string? SabirthCertificateno { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public int? Age { get; set; }

    public string? Firstname { get; set; }

    public string? Surname { get; set; }

    public int? Countryoforigin { get; set; }

    public string? Folderno { get; set; }

    public string? Specify { get; set; }

    public int? Gender { get; set; }

    public string? Cellnumber { get; set; }

    public string? Additionalcontactnumber { get; set; }

    public string? Nextofkinname { get; set; }

    public string? Nextofkinphoneno { get; set; }

    public string? Relationship { get; set; }

    public string? Bookingrefno { get; set; }

    public string? Consenthivtest { get; set; }

    public string? Consentmmc { get; set; }

    public string? Guardianname { get; set; }

    public string? Guardiansurname { get; set; }

    public int? Guardianage { get; set; }

    public bool? Guardianconsent { get; set; }

    public string? Counsellorsname { get; set; }

    public bool? Followup { get; set; }

    public string? Dataitemsfrom { get; set; }
}
