namespace JLSC.Framework.Domain.Common.Interfaces;

/// <summary>
/// Define un Aggregate Root dentro del modelo de dominio.
///
/// Un Aggregate Root es el único punto de acceso permitido para modificar
/// las entidades pertenecientes a un agregado, garantizando el cumplimiento
/// de las reglas de negocio y la consistencia del dominio.
///
/// Esta interfaz funciona como un marcador arquitectónico y no expone
/// miembros públicos.
/// </summary>
public interface IAggregateRoot
{
}