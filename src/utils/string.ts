export const toList = (values?: string | string[]): string[] => 
  // see: https://stackoverflow.com/a/14334054/47633
  typeof(values) === 'string' ? values.match(/(\\.|[^;])+/g).filter(it => it.trim()!=='') : values

export const slugify = (text: string): string => text.replace(/\s+/ig, '-').toLowerCase()
