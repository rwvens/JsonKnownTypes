﻿using JsonKnownTypes.Exceptions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonKnownTypes.UnitTests
{
    [TestFixture]
    public class SettingsTest
    {
        [Test]
        public void Contains_correct_settings_interface()
        {
            var settings = JsonKnownTypesSettingsManager.GetDiscriminatorValues<ISettings>();
            
            Assert.True(settings.Count == 0);
            Assert.AreEqual(settings.FieldName, "name");
        }

        [Test]
        public void Throw_an_exception() =>
            Assert.Throws<JsonKnownTypesException>(delegate
            {
                JsonKnownTypesSettingsManager.GetDiscriminatorValues<Settings>();
            });
    }

    [JsonConverter(typeof(JsonKnownTypesConverter<ISettings>))]
    [JsonDiscriminator(Name = "name")]
    public interface ISettings
    {
        string Summary { get; set; }
    }

    [JsonConverter(typeof(JsonKnownTypesConverter<Settings>))]
    [JsonDiscriminator(Name = "NotName")]
    public class Settings
    {
        string Summary { get; set; }
    }

    [JsonKnownThisType("Settings")] //same discriminator as for Settings should throw
    public class Settings1 : Settings
    { }
}
