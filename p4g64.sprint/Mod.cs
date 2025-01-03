using p4g64.sprint.Configuration;
using p4g64.sprint.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Mod.Interfaces;
using static p4g64.sprint.Utils;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace p4g64.sprint;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public unsafe class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private IHook<MovePlayerDelegate> _movePlayerHook;
    private IHook<GetRunAnimationIdDelegate> _getRunAnimationHook;
    private int* _inputs;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;
        Initialise(_logger, _configuration, _modLoader);

        SigScan("40 53 48 83 EC 70 48 8B D9 44 0F 29 44 24 ?? 48 8D 0D ?? ?? ?? ?? 44 0F 28 C1", "MovePlayer", address =>
        {
            _movePlayerHook = _hooks!.CreateHook<MovePlayerDelegate>(MovePlayer, address).Activate();
        });

        SigScan("8B 05 ?? ?? ?? ?? 0F BA E0 0E 73 ?? B8 03 00 00 00", "InputsPtr", address =>
        {
            _inputs = (int*)GetGlobalAddress(address + 2) + 2;
        });

        SigScan("40 53 48 83 EC 20 8B D9 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 83 FB 01 0F 85 ?? ?? ?? ??", "GetRunAnimationId", address =>
        {
            _getRunAnimationHook = _hooks!.CreateHook<GetRunAnimationIdDelegate>(GetRunAnimationId, address).Activate();
        });
    }

    private void MovePlayer(nuint param_1, float speed)
    {
        if (IsSprinting())
            speed *= _configuration.SprintMultiplier;
        _movePlayerHook.OriginalFunction(param_1, speed);
    }

    private int GetRunAnimationId(int partyMember)
    {
        if (!IsSprinting())
            return _getRunAnimationHook.OriginalFunction(partyMember);

        return _configuration.SprintAnimation;
    }

    private bool IsSprinting()
    {
        return (*_inputs >> _configuration.SprintButton & 1) != 0;
    }

    private delegate void MovePlayerDelegate(nuint param_1, float speed);
    private delegate int GetRunAnimationIdDelegate(int partyMember);

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}