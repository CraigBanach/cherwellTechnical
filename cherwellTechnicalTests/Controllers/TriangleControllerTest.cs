using cherwellTechnical.Controllers;
using cherwellTechnical.Factories.Interfaces;
using cherwellTechnical.Models;
using Moq;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace cherwellTechnicalTests.Controllers
{
    public class TriangleControllerTest
    {
        public class GetWithDesignation
        {
            private readonly Mock<ITriangleFactory> factory;
            private readonly TriangleController controller;

            public GetWithDesignation()
            {
                this.factory = new Mock<ITriangleFactory>();
                this.factory.Setup( factory =>
                     factory.ToTriangle( It.Is<string>( val => val == "A1" ) ) )
                        .Returns( new Triangle() );
                this.factory.Setup( factory =>
                      factory.ToTriangle( It.Is<string>( val => val == "" ) ) )
                        .Throws( new ArgumentException() );
                this.factory.Setup( factory =>
                    factory.ToTriangle( "ABC" ) )
                        .Throws( new DivideByZeroException() );



                this.controller = new TriangleController( this.factory.Object );
            }

            [Fact]
            public void ReturnsBadRequestIfInputIsBad()
            {
                Assert.IsType<BadRequestResult>( this.controller.Get( "" ) );
            }

            [Fact]
            public void Returns500IfExceptionIsThrown()
            {
                Assert.Equal( 
                    500, 
                    ( this.controller.Get( "ABC" ) as StatusCodeResult ).StatusCode );
            }

            [Fact]
            public void ReturnsOkIfValidValuesPassed()
            {
                Assert.IsType<OkObjectResult>( this.controller.Get( "A1" ) );
            }
        }

        public class PostWithCoordinates
        {
            private readonly Mock<ITriangleFactory> factory;
            private readonly TriangleController controller;

            public PostWithCoordinates()
            {
                this.factory = new Mock<ITriangleFactory>();

                this.factory.Setup( factory =>
                    factory.ToTriangle( It.Is<Collection<Coordinate>>( val => val.Count == 0 ) ) )
                        .Throws( new ArgumentException() );
                this.factory.Setup( factory =>
                      factory.ToTriangle( It.Is<Collection<Coordinate>>( val => val.Count == 1 ) ) )
                        .Throws( new DivideByZeroException() );
                this.factory.Setup( factory =>
                    factory.ToTriangle( It.Is<Collection<Coordinate>>( val => val.Count == 3 ) ) )
                        .Returns( new Triangle() );

                this.controller = new TriangleController( this.factory.Object );
            }

            [Fact]
            public void ReturnsBadRequestIfInputIsBad()
            {
                Assert.IsType<BadRequestResult>( this.controller.Post( new Collection<Coordinate>() ) );
            }

            [Fact]
            public void Returns500IfExceptionIsThrown()
            {
                Assert.Equal(
                    500,
                    ( this.controller.Post(
                        new Collection<Coordinate>()
                        {
                            new Coordinate(10, 20)
                        }
                    ) as StatusCodeResult ).StatusCode );
            }

            [Fact]
            public void ReturnsOkIfValidValuesPassed()
            {
                Assert.IsType<OkObjectResult>( this.controller.Post(
                    new Collection<Coordinate>()
                    {
                        new Coordinate(10, 20),
                        new Coordinate(10, 20),
                        new Coordinate(10, 20)
                    }
                ));
            }
        }
    }
}
