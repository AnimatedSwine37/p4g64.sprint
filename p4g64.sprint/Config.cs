using p4g64.sprint.Template.Configuration;
using System.ComponentModel;

namespace p4g64.sprint.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;

    [DisplayName("Sprint button")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(13)]
    public int SprintButton { get; set; } = 13;

    [DisplayName("Sprint Animation")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(5)]
    public int SprintAnimation { get; set; } = 5;
    
    [DisplayName("Sprint Multiplier")]
    [Description("The multiplier for the player's speed when they are sprinting. This should be above 1 so you actually go faster :)")]
    [DefaultValue(2f)]
    public float SprintMultiplier { get; set; } = 2f;

}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}