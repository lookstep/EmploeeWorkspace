﻿using EmoloyeeTask.Data.Interfaces;
using EmployeeTask.API.Controllers;
using EmployeeTask.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace EmoloyeeTask.API.Tests
{
    public class AssigmentControllerTest
    {
        [Fact]
        public void GetAllAssigmentStatusInternalServerError()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.GetAll()).Throws(new Exception());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.GetTasksForEmployees().GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Ошибка получения данных с сервера", ((ObjectResult)result.Result!).Value!.ToString());
        }
        [Fact]
        public void GetAssigmentStatusInternalServerError()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(1)).Throws(new Exception());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.GetTaskForEmployeeById(id: 1).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Ошибка получения данных с сервера", ((ObjectResult)result.Result!).Value!.ToString());
        }
        [Fact]
        public void CreateAssigmentStatusInternalServerError()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Add(new Assignment())).Throws(new Exception());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            var randName = Guid.NewGuid().ToString();
            var createdAssignment = new Assignment() { TaskName = randName };
            //act

            var result = assignmentController.CreateTasksForEmployees(taskForEmployee: createdAssignment).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Ошибка создания задачи", ((ObjectResult)result.Result!).Value!.ToString());
        }
        [Fact]
        public void UpdateAssigmentStatusInternalServerError()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Update(null!)).Throws(new Exception());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            var randName = Guid.NewGuid().ToString();
            //act

            var result = assignmentController.UpdateTasksForEmployee(id: 0, taskForEmployee: null!).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Ошибка обновления задачи", ((ObjectResult)result.Result!).Value!.ToString());
        }
        [Fact]
        public void DeleteAssigmentStatusInternalServerError()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(1)).Throws(new Exception());
            mock.Setup(x => x.Delete(1)).Throws(new Exception());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.DeleteTasksForEmployee(id: 1).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
            Assert.Equal("Ошибка удаления задачи", ((ObjectResult)result).Value!.ToString());
        }
        [Fact]
        public void GetAssigmentStatusOk()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(1)).ReturnsAsync(new Assignment());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.GetTaskForEmployeeById(id: 1).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result.Result!).StatusCode);
        }
        [Fact]
        public void GetAllAssigmentStatusOK()
        {
            //arrange
            Assignment[] assignments = new Assignment[] { new Assignment() };
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.GetAll()).ReturnsAsync(assignments);

            AssignmentController assignmentController = new AssignmentController(mock.Object);
            //act
            var result = assignmentController.GetTasksForEmployees().GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result.Result!).StatusCode);
        }
        [Fact]
        public void UpdateAssigmentStatusOK()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var updatedAssignment = new Assignment() { TaskName = randName, Id = 1 };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Update(updatedAssignment)).ReturnsAsync(updatedAssignment);
            mock.Setup(x => x.Get(1)).ReturnsAsync(new Assignment());

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.UpdateTasksForEmployee(id: 1, taskForEmployee: updatedAssignment).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result.Result!).StatusCode);
        }
        [Fact]
        public void DeleteAssigmentStatusOk()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var deletedAssignment = new Assignment() { TaskName = randName, Id = 1 };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(1)).ReturnsAsync(deletedAssignment);
            mock.Setup(x => x.Delete(1));

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.DeleteTasksForEmployee(id: 1).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result).StatusCode);
            Assert.Equal("Задача с id 1 была успешно удалена", ((ObjectResult)result).Value!.ToString());
        }

        [Fact]
        public void CreateAssigmentStatusCreated()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var createdAssignment = new Assignment() { TaskName = randName };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Add(createdAssignment)).ReturnsAsync(createdAssignment);

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.CreateTasksForEmployees(taskForEmployee: createdAssignment).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status201Created, ((ObjectResult)result.Result!).StatusCode);
        }
        [Fact]
        public void CreateAssignmentStatusBadRequest()
        {
            //arrange

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Add(null!));

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.CreateTasksForEmployees(taskForEmployee: null!).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Данные о задаче пустые или некорректно введены", ((ObjectResult)result.Result).Value!.ToString());
        }
        [Fact]
        public void UpdateAssignmentStatusBadRequest()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var updatedAssignment = new Assignment() { TaskName = randName, Id = 2 };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Update(updatedAssignment)).ReturnsAsync(updatedAssignment);
            mock.Setup(x => x.Get(2)).ReturnsAsync(updatedAssignment);

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.UpdateTasksForEmployee(id: 1, taskForEmployee: updatedAssignment).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("id у задачи и зпроса различны", ((ObjectResult)result.Result).Value!.ToString());
        }
        [Fact]
        public void GetAssignmentStatusNotFound()
        {
            //arrange
            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(0));

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.GetTaskForEmployeeById(id: 1).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Задачи с данным id нет", ((ObjectResult)result.Result!).Value!.ToString());
        }
        [Fact]
        public void UpdateAssignmentStatusNotFound()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var updatedAssignment = new Assignment() { TaskName = randName, Id = 2 };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Update(updatedAssignment)).ReturnsAsync(updatedAssignment);
            mock.Setup(x => x.Get(1)).ReturnsAsync(updatedAssignment);

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.UpdateTasksForEmployee(id: 2, taskForEmployee: updatedAssignment).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result.Result!).StatusCode);
            Assert.Equal("Задачи с данным id нет", ((ObjectResult)result.Result).Value!.ToString());
        }
        [Fact]
        public void DeleteAssignmentStatusNotFound()
        {
            //arrange
            var randName = Guid.NewGuid().ToString();
            var deletedAssignment = new Assignment() { TaskName = randName, Id = 2 };

            Mock<IDbRepository<Assignment>> mock = new Mock<IDbRepository<Assignment>>();
            mock.Setup(x => x.Get(1)).ReturnsAsync(deletedAssignment);
            mock.Setup(x => x.Delete(2));

            AssignmentController assignmentController = new AssignmentController(mock.Object);

            //act
            var result = assignmentController.DeleteTasksForEmployee(id: 2).GetAwaiter().GetResult();

            //assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
            Assert.Equal("Задачи с данным id нет", ((ObjectResult)result).Value!.ToString());
        }
    }
}
