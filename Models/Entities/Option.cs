namespace QuizApp.Models.Entities
{
	public record Option
	{
		public Guid Id { get; init; }
		public required string Text { get; init; }

		// Relationship
		public Guid QuestionId { get; init; }
	}
}
