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
    [Description("Thee button to press to sprint. Changing in game key-binds changes what these mean.")]
    [DefaultValue(InputType.Circle)]
    public InputType SprintButton { get; set; } = InputType.Circle;
    
    [DisplayName("Toggle Sprint")]
    [Description("If set to true then pressing the sprint button will toggle it. Otherwise you sprint only when it is held.")]
    [DefaultValue(false)]
    public bool ToggleSprint { get; set; } = false;
    
    // TODO make an animation one day, would be cool
    // [DisplayName("Sprint Animation")]
    // [Description("Logs additional information to the console that is useful for debugging.")]
    // [DefaultValue(5)]
    // public int SprintAnimation { get; set; } = 5;
    
    [DisplayName("Sprint Multiplier")]
    [Description("The multiplier for the player's speed when they are sprinting. This should be above 1 so you actually go faster :)")]
    [DefaultValue(1.3)]
    public double SprintMultiplier { get; set; } = 1.3;

}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}