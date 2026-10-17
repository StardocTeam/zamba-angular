export const USE_MOCK_DATA = false;

export const REQUEST_ACTION_REPORTS = { approval: 11535116, confirmation: 11535117, summary: 11535126 } as const;
export const REQUEST_ACTION_RULES = {
  approval: { approve: 1223308, reject: 11546880 },
  confirmation: { approve: 1021731, reject: 11546880 },
  summary: { approve: 11547820, reject: 11547815 },
} as const;

export const SUMMARY_COMMENT_RULE = 11547418;
