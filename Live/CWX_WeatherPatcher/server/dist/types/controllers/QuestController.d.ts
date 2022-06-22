import { DialogueHelper } from "../helpers/DialogueHelper";
import { ItemHelper } from "../helpers/ItemHelper";
import { ProfileHelper } from "../helpers/ProfileHelper";
import { QuestConditionHelper } from "../helpers/QuestConditionHelper";
import { QuestHelper } from "../helpers/QuestHelper";
import { IPmcData } from "../models/eft/common/IPmcData";
import { IQuest } from "../models/eft/common/tables/IQuest";
import { IItemEventRouterResponse } from "../models/eft/itemEvent/IItemEventRouterResponse";
import { IAcceptQuestRequestData } from "../models/eft/quests/IAcceptQuestRequestData";
import { ICompleteQuestRequestData } from "../models/eft/quests/ICompleteQuestRequestData";
import { IHandoverQuestRequestData } from "../models/eft/quests/IHandoverQuestRequestData";
import { ILogger } from "../models/spt/utils/ILogger";
import { ItemEventRouter } from "../routers/ItemEventRouter";
import { ConfigServer } from "../servers/ConfigServer";
import { DatabaseServer } from "../servers/DatabaseServer";
import { PlayerService } from "../services/PlayerService";
import { TimeUtil } from "../utils/TimeUtil";
export declare class QuestController {
    private logger;
    private timeUtil;
    private itemEventRouter;
    private databaseServer;
    private itemHelper;
    private dialogueHelper;
    private profileHelper;
    private questHelper;
    private questConditionHelper;
    private playerService;
    private configServer;
    private questConfig;
    constructor(logger: ILogger, timeUtil: TimeUtil, itemEventRouter: ItemEventRouter, databaseServer: DatabaseServer, itemHelper: ItemHelper, dialogueHelper: DialogueHelper, profileHelper: ProfileHelper, questHelper: QuestHelper, questConditionHelper: QuestConditionHelper, playerService: PlayerService, configServer: ConfigServer);
    getClientQuests(sessionID: string): IQuest[];
    acceptQuest(pmcData: IPmcData, acceptedQuest: IAcceptQuestRequestData, sessionID: string): IItemEventRouterResponse;
    acceptRepeatableQuest(pmcData: IPmcData, acceptedQuest: IAcceptQuestRequestData, sessionID: string): IItemEventRouterResponse;
    completeQuest(pmcData: IPmcData, body: ICompleteQuestRequestData, sessionID: string): IItemEventRouterResponse;
    handoverQuest(pmcData: IPmcData, body: IHandoverQuestRequestData, sessionID: string): IItemEventRouterResponse;
}