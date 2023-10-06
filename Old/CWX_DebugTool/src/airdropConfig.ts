import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";

import{ IAirdropConfig } from "@spt-aki/models/spt/config/IAirdropConfig"

import { AirdropConfig } from "models/IConfig";

import { inject, injectable } from "tsyringe";
import { CwxConfigHandler } from "./configHandler";

@injectable()
export class CwxAirdropConfig
{
    private tables: IAirdropConfig;
    private config: AirdropConfig;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().airdropConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.AIRDROP);

        this.enableAllTheTime();
        this.changeFlightHeight();
        this.changeStartTime();
        this.changePlaneVolume();
    }

    private enableAllTheTime(): void
    {
        if (this.config.enableAllTheTime)
        {
            for (const i in this.tables.airdropChancePercent)
            {
                this.tables.airdropChancePercent[i] = 100;
            }
        }
    }

    private changeFlightHeight(): void
    {
        if (this.config.changeFlightHeight)
        {
            this.tables.planeMinFlyHeight = 100;
            this.tables.planeMaxFlyHeight = 110;
        }
    }

    private changeStartTime(): void
    {
        if (this.config.changeStartTime)
        {
            this.tables.airdropMinStartTimeSeconds = 10;
            this.tables.airdropMaxStartTimeSeconds = 20;
        }
    }

    private changePlaneVolume(): void
    {
        if (this.config.changePlaneVolume)
        {
            this.tables.planeVolume = 0.1;
        }
    }
}