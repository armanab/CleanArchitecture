using CleanApplication.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
