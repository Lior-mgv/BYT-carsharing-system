﻿using CarsharingSystem.Model;

namespace TestProject;

public class ConnectionTests
{
    [Test]
public void AddAddress_ShouldAddAddressAndUpdateBidirectionalRelationship()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());
    var address = new Address("City", "123", 12345, "12345");

    // Act
    offer.AddAddress(address);

    // Assert
    Assert.Contains(address, offer.Addresses, "Address should be added to Offer.");
    Assert.Contains(offer, address.Offers, "Offer should be added to Address.");
}

[Test]
public void AddAddress_ShouldNotAddDuplicateAddress()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());
    var address = new Address("City", "123", 12345, "12345");
    offer.AddAddress(address);
    

    // Assert
    Assert.Throws<InvalidOperationException>(() => offer.AddAddress(address));
}

[Test]
public void DeleteAddress_ShouldRemoveAddressAndUpdateBidirectionalRelationship()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());
    var address = new Address("City", "123", 12345, "12345");
    offer.AddAddress(address);

    // Act
    var result = offer.DeleteAddress(address);

    // Assert
    Assert.IsTrue(result, "DeleteAddress should return true if the address was removed.");
    Assert.IsFalse(offer.Addresses.Contains(address), "Address should be removed from Offer.");
    Assert.IsFalse(address.Offers.Contains(offer), "Offer should be removed from Address.");
}

[Test]
public void DeleteAddress_ShouldHandleNonexistentAddressGracefully()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());
    var address = new Address("City", "123", 12345, "12345");

    // Act
    var result = offer.DeleteAddress(address);

    // Assert
    Assert.IsFalse(result, "DeleteAddress should return false if the address was not found.");
    Assert.IsEmpty(offer.Addresses, "Offer addresses should remain unchanged.");
}

[Test]
public void AddAddress_ShouldThrowExceptionForNullInput()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => offer.AddAddress(null));
}

[Test]
public void DeleteAddress_ShouldThrowExceptionForNullInput()
{
    // Arrange
    var offer = new Offer(100.0m, "Test Offer", 18, new PassengerCar());

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => offer.DeleteAddress(null));
}

}