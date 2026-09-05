namespace DnD.Combat.Exceptions;

/// <summary>
/// The exception thrown when a character attempts to perform an action
/// without having enough mana.
/// </summary>
/// <param name="message">
/// A message describing the action and why the available mana was insufficient.
/// </param>
public class InsufficientManaException(string message) : Exception(message);