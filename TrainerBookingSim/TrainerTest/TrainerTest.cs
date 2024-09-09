using BusinessLogic;

namespace TrainerTest;

public class TestTrainer
{
    [Fact]
    public void FreePlaces_ReturnCorrectNumberofFreePlaces()
    {
        // Arrange
        var trainer = new Trainer { MaxTicket = 10, OccupiedPlaces = 4 };

        // Act
        var freePlaces = trainer.FreePlaces;

        // Assert
        Assert.Equal(6, freePlaces);
    }
    [Fact]
    public void OccupyPlace_IncreasesOccupiedPlaces_WhenPlacesAreAvailable()
    {
        //Arrange
        var trainer = new Trainer { MaxTicket = 10, OccupiedPlaces = 4 };
        
        //Act
        var result = trainer.OccupyPlace();
        
        //Assert
        Assert.True(result);
        Assert.Equal(5, trainer.OccupiedPlaces);
    }

    [Fact]
    public void OccupyPlace_DoesNotIncreaseOccupiedPlaces_WhenPlacesAreNotAvailable()
    {
        //Arrange
        var trainer = new Trainer { MaxTicket = 10, OccupiedPlaces = 10 };
        
        //Act
        var result = trainer.OccupyPlace();
        
        //Assert
        Assert.False(result);
        Assert.Equal(10, trainer.OccupiedPlaces);
    }
}