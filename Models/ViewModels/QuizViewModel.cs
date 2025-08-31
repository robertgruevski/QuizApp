using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuizApp.Models.ViewModels
{
	public class QuizViewModel
	{
		public required List<QuestionItem> Questions { get; set; }
	}
}

public class QuestionItem
{
	public required Guid Id { get; set; }
	public required string Text { get; set; }
	public required List<SelectListItem> Options { get; set; }
}