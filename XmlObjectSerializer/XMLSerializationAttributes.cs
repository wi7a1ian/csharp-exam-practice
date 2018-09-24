using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlObjectSerializerTest
{
    // The XmlRootAttribute allows you to set an alternate name 
    // (PurchaseOrder) for the XML element and its namespace. By 
    // default, the XmlSerializer uses the class name. The attribute 
    // also allows you to set the XML namespace for the element. Lastly,
    // the attribute sets the IsNullable property, which specifies whether 
    // the xsi:null attribute appears if the class instance is set to 
    // a null reference.
    [XmlRootAttribute("PurchaseOrder", Namespace = "http://www.cpandl.com",
    IsNullable = false)]
    public class PurchaseOrder
    {
        public Address ShipTo;
        public string OrderDate;
        // The XmlArrayAttribute changes the XML element name
        // from the default of "OrderedItems" to "Items".
        [XmlArrayAttribute("Items")]
        public OrderedItem[] OrderedItems;
        public decimal SubTotal;
        public decimal ShipCost;
        public decimal TotalCost;
    }

    public class Address
    {
        // The XmlAttribute instructs the XmlSerializer to serialize the 
        // Name field as an XML attribute instead of an XML element (the 
        // default behavior).
        [XmlAttribute]
        public string Name;
        public string Line1;

        // Setting the IsNullable property to false instructs the 
        // XmlSerializer that the XML attribute will not appear if 
        // the City field is set to a null reference.
        [XmlElementAttribute(IsNullable = false)]
        public string City;
        public string State;
        public string Zip;
    }

    public class OrderedItem
    {
        public string ItemName;
        public string Description;
        public decimal UnitPrice;
        public int Quantity;
        public decimal LineTotal;

        // Calculate is a custom method that calculates the price per item
        // and stores the value in a field.
        public void Calculate()
        {
            LineTotal = UnitPrice * Quantity;
        }
    }
}
/*
<?xml version="1.0" encoding="utf-8"?>
<PurchaseOrder xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.cpandl.com">
    <ShipTo Name="Teresa Atkinson">
        <Line1>1 Main St.</Line1>
        <City>AnyTown</City>
        <State>WA</State>
        <Zip>00000</Zip>
    </ShipTo>
    <OrderDate>Wednesday, June 27, 2001</OrderDate>
    <Items>
        <OrderedItem>
            <ItemName>Widget S</ItemName>
            <Description>Small widget</Description>
            <UnitPrice>5.23</UnitPrice>
            <Quantity>3</Quantity>
            <LineTotal>15.69</LineTotal>
        </OrderedItem>
    </Items>
    <SubTotal>15.69</SubTotal>
    <ShipCost>12.51</ShipCost>
    <TotalCost>28.2</TotalCost>
</PurchaseOrder>
*/