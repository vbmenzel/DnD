namespace DnD.Combat.Exceptions;

/// <summary>
/// The exception thrown when a defeated character attempts to perform an action.
/// </summary>
/// <param name="message">
/// A message describing the action that could not be performed because the
/// character was defeated.
/// </param>
public class CharacterIsDefeatedException(string message) : Exception(message);