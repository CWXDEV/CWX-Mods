import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { IInRaidConfig } from "@spt-aki/models/spt/config/IInRaidConfig";

import { CWX_ConfigHandler } from "./configHandler";
import { inraidConfig } from "models/IConfig";

@injectable()
export class CWX_InraidConfig
{
    private tables: IInRaidConfig;
    private config: inraidConfig;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().inraidConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.IN_RAID);

        this.TurnPVEOff();
    }

    private TurnPVEOff(): void
    {
        if (this.config.turnPVEOff)
        {
            this.tables.raidMenuSettings.enablePve = false;
        }
    }
}