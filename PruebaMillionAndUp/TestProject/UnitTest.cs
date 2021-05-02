using BE.Bases;
using BE.Request;
using BE.RSV;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using PruebaMillionAndUp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
    public class Tests
    {
        private IConfiguration _config;

        [SetUp]
        public void Setup()
        {
            if (this._config == null)
            {
                var buider = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                _config = buider.Build();
            }
        }

        [Test]
        public async Task TestOwner()
        {
            OwnerController ownerController = new(this._config);

            using var stream = File.OpenRead(@"D:\Usuario\OneDrive\Imágenes\foto de perfil.jpg");
            FormFile PhotoFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            OwnerRequest request = new()
            {
                PhotoFile = PhotoFile,
                Owner = JsonConvert.SerializeObject(new Owner
                {
                    Name = "Carlos Andres Tobon Alzate",
                    Adress = "Calle 40C # 58C 20, Bello, Antioquia, Colombia",
                    Birthday = DateTime.Parse("21/07/1991"),
                    Email = "carlostoalz@hotmail.com",
                    Passord = "lamesaesfea"
                })
            };

            RSV<Owner> rsv = (await ownerController.InsertOwner(request)).Value;

            if (rsv.Exitoso && rsv.Datos != null)
            {
                Assert.Pass("Se creo correctamente el usuario");
            }
            else
            {
                Assert.Fail(rsv.Error.Mensaje);
            }
        }

        [Test]
        public async Task TestLogin()
        {
            AuthController authController = new(this._config);

            RSV<string> rsv = (await authController.Login(new() { Email = "carlostoalz@hotmail.com", Passord = "lamesaesfea" })).Value;

            if (rsv.Exitoso)
            {
                if (!string.IsNullOrEmpty(rsv.Datos))
                {
                    Assert.Pass("Session iniciada token generado");
                }
                else
                {
                    Assert.Fail("No se generó token");
                }
            }
            else
            {
                Assert.Fail(rsv.Error.Mensaje);
            }
        }

        [Test]
        public async Task TestInsertProperty()
        {
            PropertyRequest propertyRequest = new();

            List<IFormFile> images = new();

            using var stream = File.OpenRead(@"D:\Usuario\OneDrive\Imágenes\Apartamento\WhatsApp Unknown 2021-04-11 at 10.23.09 AM\WhatsApp Image 2021-03-27 at 8.52.30 PM.jpeg");
            FormFile image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            images.Add(image);

            propertyRequest.Images = images;
            propertyRequest.SaveImagesInDB = false;
            propertyRequest.Property = JsonConvert.SerializeObject(new Property
            {
                Name = "Apartamento bello bucaros 1",
                Adress = "Carrera 58A # 40A 59 Apto. 301, Bello, Antioquia, Colombia",
                Value = 134453782,
                Tax = 0.19m,
                Year = 1994
            });

            var fakeHttpContext = new Mock<HttpContext>();
            var fakeIdentity = new GenericIdentity("1");
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new ControllerContext
            {
                HttpContext = fakeHttpContext.Object
            };
            PropertyController controller = new(this._config, null);
            controller.ControllerContext = controllerContext;

            RSV<Property> rsv = (await controller.InsertProperty(propertyRequest)).Value;

            if (rsv.Exitoso)
            {
                Assert.Pass("Se creo la propiedad");
            }
            else
            {
                Assert.Fail(rsv.Error.Mensaje);
            }
        }

        [Test]
        public async Task TestGetProperty()
        {
            var fakeHttpContext = new Mock<HttpContext>();
            var fakeIdentity = new GenericIdentity("1");
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new ControllerContext
            {
                HttpContext = fakeHttpContext.Object
            };
            PropertyController controller = new(this._config, null);
            controller.ControllerContext = controllerContext;

            RSV<Property> rsv = (await controller.GetProperty(1)).Value;

            if (rsv.Exitoso)
            {
                Assert.Pass("Se consultó la propiedad");
            }
            else
            {
                Assert.Fail(rsv.Error.Mensaje);
            }
        }

        [Test]
        public async Task TestGetProperties()
        {
            var fakeHttpContext = new Mock<HttpContext>();
            var fakeIdentity = new GenericIdentity("1");
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new ControllerContext
            {
                HttpContext = fakeHttpContext.Object
            };
            PropertyController controller = new(this._config, null);
            controller.ControllerContext = controllerContext;

            RSV<IEnumerable<Property>> rsv = (await controller.GetProperties()).Value;

            if (rsv.Exitoso)
            {
                Assert.Pass("Se consultó la propiedad");
            }
            else
            {
                Assert.Fail(rsv.Error.Mensaje);
            }
        }
    }
}