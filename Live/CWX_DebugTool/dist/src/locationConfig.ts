import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { ILocationConfig } from "@spt-aki/models/spt/config/ILocationConfig";

import { CwxConfigHandler } from "./configHandler";
import { LocationConfig } from "models/IConfig";

@injectable()
export class CwxLocationConfig
{
    private tables: ILocationConfig;
    private config: LocationConfig;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().locationConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.LOCATION);

        this.turnLootOff();
    }

    private turnLootOff(): void
    {
        if (this.config.turnLootOff)
        {
            for (const location in this.tables.looseLootMultiplier) {
                this.tables.looseLootMultiplier[location] = 0;
            }

            for (const location in this.tables.staticLootMultiplier) {
                this.tables.staticLootMultiplier[location] = 0;
            }
        }
    }
}