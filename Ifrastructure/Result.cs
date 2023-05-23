using System;

namespace BELOBORODOV_DIPLOM.Ifrastructure {
	/// <summary>
	/// Результат выполнения операции
	/// </summary>
	public class Result {
		/// <summary>
		/// Общий положительный результат операции
		/// </summary>
		private static Result _ok = new Result(true, "Успех");

		/// <summary>
		/// Флаг успеха выполнения
		/// </summary>
		public bool Success { get; protected set; }

		/// <summary>
		/// Описание результата выполнения операции
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="success"></param>
		/// <param name="description"></param>
		public Result(bool success, string description) {
			Success = success;
			Description = description;
		}

		/// <summary>
		/// Добавить текст к сообщению
		/// </summary>
		/// <param name="text">Текст для добавления</param>
		public void AppendMessage(string text) {
			if (Description == null)
				Description = text;
			else
				Description += text;
		}

		/// <summary>
		/// Общий Положительный результат операции
		/// </summary>
		public static Result Ok { get { return _ok; } }

		public static Result OK(string msg = "Успех") => new Result(true, msg);

		/// <summary>
		/// Опреация прошла без успеха
		/// </summary>
		/// <param name="msg">Сообщение</param>
		/// <returns></returns>
		public static Result Fail(string msg) => new Result(false, msg);


		/// <summary>
		/// Опрерация вызвала исключение
		/// </summary>
		/// <param name="msg">Сообщение</param>
		/// <param name="ex">Вызванное исключение</param>
		/// <returns></returns>
		public static Result Fail(string msg, Exception ex) => new Result(false, $"{msg} {FlattenErrMessage(ex)}");

		/// <summary>
		/// Развертывает текстовое описание исключения, включая внутренние исключения
		/// </summary>
		/// <returns></returns>
		public static string FlattenErrMessage(Exception e) {
			string rez = "";
			while (e != null) {
				rez += e.Message + Environment.NewLine;
				e = e.InnerException;
			}
			return rez;

		}
	}

	/// <summary>
	/// Обобщенный результат операции
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Result<T> : Result {
		/// <summary>
		/// Данные возвращенные метеодом
		/// </summary>
		public T OutData { get; private set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="success"></param>
		/// <param name="description"></param>
		/// <param name="outData"></param>
		public Result(bool success, string description, T outData = default) : base(success, description) {
			OutData = outData;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public Result(string msg, Exception ex)
				: base(false, $"{msg} {FlattenErrMessage(ex)}") { }

		/// <summary>
		/// Успешное выполнение команды с выходными данными
		/// </summary>
		/// <param name="outData"></param>
		public Result(T outData) : base(true, "Успех.") {
			OutData = outData;
		}

		/// <summary>
		/// Успешный результат выполнения операции
		/// </summary>
		/// <param name="outData"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public static Result<T> OK(T outData, string msg = "Успех.") {
			return new Result<T>(true, msg, outData);
		}

		/// <summary>
		/// Неуспешное выполнение операции из-за исключения
		/// </summary>
		/// <param name="msg">Сообщение с причиной</param>
		/// <param name="ex">Возникшее исключение</param>
		/// <param name="outData">Возвращаемые данные, если есть</param>
		/// <returns></returns>
		public static Result<T> Fail(string msg, Exception ex, T outData = default) => new Result<T>(msg, ex) { OutData = outData };

		/// <summary>
		/// Неуспешное выполнение операции
		/// </summary>
		/// <param name="msg">Сообщение с причиной</param>
		/// <param name="outData">Возвращаемые данные, если есть</param>
		/// <returns></returns>
		public static Result<T> Fail(string msg, T outData = default) => new Result<T>(false, msg, outData);

		/// <summary>
		/// Неуспешное выполнение операции
		/// </summary>
		/// <param name="result">Сообщение с причиной</param>
		/// <returns></returns>
		public static Result<T> Fail(Result result) => new Result<T>(false, result.Description);
	}
}
