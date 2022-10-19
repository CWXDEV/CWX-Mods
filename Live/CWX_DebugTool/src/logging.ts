import { SptLogger } from "@spt-aki/models/spt/logging/SptLogger";
import { ICwxConfig } from "models/IConfig";
import { inject, injectable } from "tsyringe";
import { CwxConfigHandler } from "./configHandler";

@injectable()
export class CwxLogging
{
    private config: ICwxConfig;

    constructor(
        @inject("WinstonLogger") private logger: SptLogger,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public sendLogging(): void
    {
        this.config = this.configHandler.getConfig();

        // globals
        this.noFallDamage();
        this.openFlea();
        this.quickScav();

        // ragfair
        this.staticTrader();
        this.roublesOnly();
        this.disableBSGBlacklist();

        // location
        this.turnLootOff();

        // inraid
        this.turnPVEOff();

        // items
        this.changeShrapProps();
        this.changeMaxAmmoForKS23();
        this.removeDevFromBlacklist();

        // airdrops
        this.enableAllTheTime();
        this.changeFlightHeight();
        this.changeStartTime();
        this.changePlaneVolume();

    }
    
    private noFallDamage(): void
    {
        if (this.config.globalsConfig.noFallDamage) 
        {
            this.logger.info("No Fall Damage Activated");
        }
    }

    private openFlea(): void 
    {
        if (this.config.globalsConfig.openFlea) 
        {
            this.logger.info("Open Flea Activated");   
        }
    }

    private quickScav(): void
    {
        if (this.config.globalsConfig.quickScav)
        {
            this.logger.info("Quick Scav Activated");
        }
    }

    private staticTrader(): void 
    {
        if (this.config.ragfairConfig.staticTrader)
        {
            this.logger.info("Static Trader Activated");
        }
    }

    private roublesOnly(): void 
    {
        if (this.config.ragfairConfig.roublesOnly)
        {
            this.logger.info("Roubles Only Activated");
        }
    }

    private disableBSGBlacklist(): void 
    {
        if (this.config.ragfairConfig.disableBSGBlacklist)
        {
            this.logger.info("Disable BSG Blacklist Activated");
        }
    }

    private turnLootOff(): void 
    {
        if (this.config.locationConfig.turnLootOff)
        {
            this.logger.info("Turn Loot Off Activated");
        }
    }

    private turnPVEOff(): void
    {
        if (this.config.inraidConfig.turnPVEOff)
        {
            this.logger.info("Turn PVE Off Activated");
        }
    }

    private changeShrapProps(): void
    {
        if (this.config.itemsConfig.changeShrapProps)
        {
            this.logger.info("Change Shrap Props Activated");
        }
    }

    private changeMaxAmmoForKS23(): void
    {
        if (this.config.itemsConfig.changeMaxAmmoForKS23)
        {
            this.logger.info("Change Max Ammo For KS23 Activated");
        }
    }

    private removeDevFromBlacklist(): void
    {
        if (this.config.itemsConfig.removeDevFromBlacklist)
        {
            this.logger.info("Remove Dev From Blacklist Activated");
        }
    }

    private enableAllTheTime(): void
    {
        if (this.config.airdropConfig.enableAllTheTime)
        {
            this.logger.info("Enable Airdrops All The Time Activated");
        }
    }

    private changeFlightHeight(): void
    {
        if (this.config.airdropConfig.changeFlightHeight)
        {
            this.logger.info("Change Flight Height Activated");
        }
    }

    private changeStartTime(): void
    {
        if (this.config.airdropConfig.changeStartTime)
        {
            this.logger.info("Change Start Time Activated");
        }
    }
        
    private changePlaneVolume(): void
    {
        if (this.config.airdropConfig.changePlaneVolume)
        {
            this.logger.info("Change Plane Volume Activated");
        }
    }
}