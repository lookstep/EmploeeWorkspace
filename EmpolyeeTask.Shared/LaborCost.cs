namespace EmployeeTask.Shared
{
    public class LaborCost : IEntity
    {
#nullable disable
        /// <summary>
        /// Уникальный ключ
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Вторичный ключ работника
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Вторичный ключ задач(назначений)
        /// </summary>
        public int AssignmentId { get; set; }
#nullable enable
        /// <summary>
        /// Количество затрачченных часов
        /// </summary>
        public float HourCount { get; set; }
        /// <summary>
        /// Навигационное свойство сотрудника
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }
        /// <summary>
        /// Навигационное свойство задач(назначений)
        /// </summary>
        [ForeignKey(nameof(AssignmentId))]
        public virtual Assignment? Assignment { get; set; }
    }
}
