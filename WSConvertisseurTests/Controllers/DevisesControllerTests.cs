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

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un ActionResult");
            List<Devise> lesDevises = result.ToList();
            List<Devise> lesDevisesTests = new List<Devise>() { new Devise(1, "Dollar", 1.08)
                ,new Devise(2, "Franc Suisse", 1.07)
                ,new Devise(3, "Yen", 120),};

            CollectionAssert.AreEqual(lesDevises, lesDevisesTests, "Les devises ne correspondent pas");
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();

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
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.GetById(20);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); 
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult),  "Erreur est pas null");
            Assert.IsNull(result.Value, "Pas de devise"); 
        }

        [TestMethod]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, null, 4.0));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");

            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;

            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created, "Pas un ActionResult");
            Assert.AreEqual(routeResult.Value, new Devise(4, null, 4.0), "Pas un ActionResult");
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
            // Arrange
            DevisesController controller = new DevisesController();

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
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNoContent()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));
        }

        [TestMethod]
        public void Delete_NotOk_ReturnsNotFound()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));
        }

        [TestMethod]
        public void Delete_Ok_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();

            // Act
            var result = controller.Post(new Devise(4, "Rouble", 4.0));
        }
    }
}