import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { IInRaidConfig } from "@spt-aki/models/spt/config/IInRaidConfig";
import { ILocationData, ILocations } from "@spt-aki/models/spt/server/ILocations";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { InraidConfig } from "models/IConfig";

import { CwxConfigHandler } from "./configHandler";

@injectable()
export class CwxInraidConfig
{
    private tables: IInRaidConfig;
    private config: InraidConfig;
    private locations: ILocations;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler,
        @inject("DatabaseServer") private databaseServer: DatabaseServer
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().inraidConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.IN_RAID);
        this.locations = this.databaseServer.getTables().locations;

        this.turnPVEOff();
        this.extendRaidTimes();
    }

    private turnPVEOff(): void
    {
        if (this.config.turnPVEOff)
        {
            this.tables.raidMenuSettings.enablePve = false;
        }
    }

    private extendRaidTimes(): void
    {
        if (this.config.extendRaidTimes)
        {
            for (const i in this.locations)
            {
                if (i !== "base")
                {
                    this.locations[i].base.EscapeTimeLimit = 300;
                    this.locations[i].base.exit_access_time = 300;
                }
            }
        }
    }
}