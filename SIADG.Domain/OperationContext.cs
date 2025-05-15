namespace SIADG.Domain;

public sealed class OperationContext
{
    public OperationContext(
        //string idCuentaServicio, 
        string idCuentaUsuario, 
        string idUsuario, 
        string? accion
        )
    {
        //IdCuentaServicio = idCuentaServicio;
        IdCuentaUsuario = idCuentaUsuario;
        IdUsuario = idUsuario;
        Accion = accion;
    }

    //public string IdCuentaServicio { get; }
    public string IdCuentaUsuario { get; }
    public string IdUsuario { get; }
    public string? Accion { get; }

}