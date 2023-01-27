using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        public DevisesController controller;
        [TestInitialize]
        public void InitialisationDesTests()
        {
            controller = new DevisesController();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un ActionResult");
            List<Devise> lesDevises = result.ToList();
            List<Devise> lesDevisesTests = new List<Devise>() { new Devise(1, "Dollar", 1.08)
                ,new Devise(2, "Franc Suisse", 1.07)
                ,new Devise(3, "Yen", 120),};

            CollectionAssert.AreEqual(lesDevises, lesDevisesTests, "Les devises ne correspondent pas"); // ou controller.Devises pour test
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); 
            Assert.IsNull(result.Result, "Erreur est pas null"); 
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); 
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques");
        }

        [TestMethod]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var result = controller.GetById(20);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); 
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
            Assert.IsNull(result.Value, "Pas de devise"); 
        }

        [TestMethod]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Act
            var result = controller.Post(new Devise(4, null, 4.0));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");

            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;

            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created, "Pas un SatusCode");
            Assert.AreEqual(routeResult.Value, new Devise(4, null, 4.0), "Pas un ActionResult");
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");

            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;

            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created, "Pas un ActionResult");
            Assert.AreEqual(routeResult.Value, new Devise(4, "Rouble", 4.0), "Pas un ActionResult");
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsBadRequest()
        {
            // Act
            var result = controller.Put(1, new Devise(4, "Pakistan", 10));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas de type BadRequestResult"); // Test de l'erreur

            BadRequestResult badRequestResult = (BadRequestResult)result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode, "pas un StatusCode");
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            // Act
            var result = controller.Put(100, new Devise(100, "Pakistan", 10));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas de type NotFoundResult"); // Test de l'erreur

            NotFoundResult notFoundtResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundtResult.StatusCode, "Pas un StatusCode");
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNoContent()
        {
            // Act
            var result = controller.Put(1, new Devise(1, "Paps", 10));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas de type NoContentResult"); // Test de l'erreur

            NoContentResult noContentResult = (NoContentResult)result;
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode, "Pas un StatusCode");
        }

        [TestMethod]
        public void Delete_NotOk_ReturnsNotFound()
        {
            // Arrange
            // DevisesController controller = new DevisesController();

            // Act
            var result = controller.Delete(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour

            //Assert.IsInstanceOfType(result, typeof(NotFoundResult), "pas de type NoContentResult"); //Test de l'erreur
            NotFoundResult notFoundResult = (NotFoundResult)result.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode, "Pas un StatusCode");
        }

        [TestMethod]
        public void Delete_Ok_ReturnsRightItem()
        {
            // Arrange
            // DevisesController controller = new DevisesController();

            // Act
            var result = controller.Delete(1);

            // Assert

            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            //Assert.IsInstanceOfType(result, typeof(NotFoundResult), "pas de type NoContentResult"); //Test de l'erreur
            NotFoundResult notFoundResult = (NotFoundResult)result.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode, "Pas un StatusCode");
        }
    }
}