using SIADG.Domain.Interfaces;

namespace SIADG.Domain.Common;
public abstract class AuditableEntity
    : IEntity
{
    protected AuditableEntity() : this(
        DateTime.Now,
        string.Empty,
        DateTime.Now,
        string.Empty,
        true,
        DateTime.Now,
        new DateTime(2999, 12, 31, 23, 59, 59).AddMilliseconds(999999)
        )
    {
    }

    protected AuditableEntity(string creadoPor) : this(
        DateTime.Now,
        creadoPor,
        DateTime.Now,
        creadoPor,
        true,
        DateTime.Now,
        new DateTime(9999, 12, 31, 23, 59, 59).AddMilliseconds(999)
    )
    {
    }

    protected AuditableEntity(string creadoPor, DateTime vigenteDesde, DateTime vigenteHasta)
        : this(
            DateTime.Now,
            creadoPor,
            DateTime.Now,
            creadoPor,
            true,
            vigenteDesde,
            vigenteHasta
        )
    {
    }

    protected AuditableEntity(DateTime creado, string creadoPor, DateTime modificado, string modificadoPor, bool vigente, DateTime vigenteDesde, DateTime vigenteHasta)
    {
        Creado = creado;
        CreadoPor = creadoPor;
        Modificado = modificado;
        ModificadoPor = modificadoPor;
        Vigente = vigente;
        VigenteDesde = vigenteDesde;
        VigenteHasta = vigenteHasta;
    }

    /// <summary>
    /// Fecha y hora de creación de la entidad
    /// </summary>
    public DateTime Creado { get; protected set; }

    /// <summary>
    /// ID de la cuenta de usuario que creó esta entidad
    /// </summary>
    public string CreadoPor { get; protected set; }

    /// <summary>
    /// Fecha y hora de última modificación de la entidad
    /// </summary>
    public DateTime Modificado { get; protected set; }

    /// <summary>
    /// ID de la cuenta de usuario que modificó por última vez esta entidad
    /// </summary>
    public string ModificadoPor { get; protected set; }

    /// <summary>
    /// Indica si esta entidad se encuentra vigente
    /// </summary>
    public bool Vigente { get; protected set; }

    /// <summary>
    /// Fecha y hora en que esta entidad empieza a ser operativa en el sistema
    /// </summary>
    public DateTime VigenteDesde { get; protected set; }

    /// <summary>
    /// Fecha y hora en que esta entidad dejó de ser operativa en el sistema
    /// </summary>
    public DateTime VigenteHasta { get; protected set; }

    //public void SetCreationInfo(CreationResultDto creationResult)
    //{
    //    var type = GetType();
    //    var prop = type.GetProperty($"Id{type.Name}");
    //    if (prop is not null && prop.PropertyType == typeof(string) && string.IsNullOrEmpty(prop.GetValue(this) as string))
    //        prop.SetValue(this, creationResult.Id);

    //    SetCreationInfo(creationResult.Creado, creationResult.CreadoPor);
    //    SetModificationInfo(creationResult.Creado, creationResult.CreadoPor);
    //    SetVigencia(creationResult.Vigente, creationResult.VigenteDesde, creationResult.VigenteHasta);
    //}

    public void SetCreationInfo(DateTime creado)
    {
        Creado = creado;
    }

    public void SetCreationInfo(string creadoPor)
    {
        CreadoPor = creadoPor;
    }

    public void SetCreationInfo(DateTime creado, string creadoPor)
    {
        Creado = creado;
        CreadoPor = creadoPor;
    }

    //public void SetModificationInfo(ModificationResultDto modificationResult)
    //{
    //    SetModificationInfo(modificationResult.Modificado, modificationResult.ModificadoPor);
    //    SetVigencia(modificationResult.Vigente, modificationResult.VigenteDesde, modificationResult.VigenteHasta);
    //}

    public void SetModificationInfo(DateTime modificado)
    {
        Modificado = modificado;
    }

    public void SetModificationInfo(string modificadoPor)
    {
        ModificadoPor = modificadoPor;
    }

    public void SetModificationInfo(DateTime modificado, string modificadoPor)
    {
        Modificado = modificado;
        ModificadoPor = modificadoPor;
    }

    public void SetVigencia(DateTime vigenteDesde, DateTime vigenteHasta)
    {
        var now = DateTime.Now;
        Vigente = now >= vigenteDesde && now <= vigenteHasta;
        VigenteDesde = vigenteDesde;
        VigenteHasta = vigenteHasta;
    }

    public void SetVigencia(bool vigente, DateTime vigenteDesde, DateTime vigenteHasta)
    {
        Vigente = vigente;
        VigenteDesde = vigenteDesde;
        VigenteHasta = vigenteHasta;
    }
}
