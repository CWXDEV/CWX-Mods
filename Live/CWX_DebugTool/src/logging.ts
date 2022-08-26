import { SptLogger } from "@spt-aki/models/spt/logging/SptLogger";
import { IConfig } from "models/IConfig";
import { inject, injectable } from "tsyringe";
import { CWX_ConfigHandler } from "./configHandler";

@injectable()
export class CWX_Logging
{
    private config: IConfig;

    constructor(
        @inject("WinstonLogger") private logger: SptLogger,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public SendLogging(): void
    {
        this.config = this.configHandler.getConfig();

        // globals
        this.NoFallDamage();
        this.OpenFlea();
        this.QuickScav();

        // ragfair
        this.StaticTrader();
        this.RoublesOnly();
        this.DisableBSGBlacklist();

        // location
        this.TurnLootOff();

        // inraid
        this.TurnPVEOff();

        // items
        this.changeShrapProps();
        this.changeMaxAmmoForKS23();

    }
    
    private NoFallDamage(): void
    {
        if (this.config.globalsConfig.noFallDamage) 
        {
            this.logger.info("No Fall Damage Activated");
        }
    }

    private OpenFlea(): void 
    {
        if (this.config.globalsConfig.openFlea) 
        {
            this.logger.info("Open Flea Activated");   
        }
    }

    private QuickScav(): void
    {
        if (this.config.globalsConfig.quickScav)
        {
            this.logger.info("Quick Scav Activated");
        }
    }

    private StaticTrader(): void 
    {
        if (this.config.ragfairConfig.staticTrader)
        {
            this.logger.info("Static Trader Activated");
        }
    }

    private RoublesOnly(): void 
    {
        if (this.config.ragfairConfig.roublesOnly)
        {
            this.logger.info("Roubles Only Activated");
        }
    }

    private DisableBSGBlacklist(): void 
    {
        if (this.config.ragfairConfig.disableBSGBlacklist)
        {
            this.logger.info("Disable BSG Blacklist Activated");
        }
    }

    private TurnLootOff(): void 
    {
        if (this.config.locationConfig.turnLootOff)
        {
            this.logger.info("Turn Loot Off Activated");
        }
    }

    private TurnPVEOff(): void
    {
        if (this.config.inraidConfig.turnPVEOff)
        {
            this.logger.info("Turn PVE Off Activated");
        }
    }

    private changeShrapProps():void
    {
        if (this.config.itemsConfig.changeShrapProps)
        {
            this.logger.info("Change Shrap Props Activated");
        }
    }

    private changeMaxAmmoForKS23():void
    {
        if (this.config.itemsConfig.changeMaxAmmoForKS23)
        {
            this.logger.info("Change Max Ammo For KS23 Activated");
        }
    }
}