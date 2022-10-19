import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { IInRaidConfig } from "@spt-aki/models/spt/config/IInRaidConfig";

import { CwxConfigHandler } from "./configHandler";
import { InraidConfig } from "models/IConfig";

@injectable()
export class CwxInraidConfig
{
    private tables: IInRaidConfig;
    private config: InraidConfig;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().inraidConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.IN_RAID);

        this.turnPVEOff();
    }

    private turnPVEOff(): void
    {
        if (this.config.turnPVEOff)
        {
            this.tables.raidMenuSettings.enablePve = false;
        }
    }
}