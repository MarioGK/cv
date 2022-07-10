  import marked from 'marked'

export const mark = (markdown: string, inline = true): string => {
  const stripP = (html: string): string => html.replace(/^<p>/i, '').replace(/<\/p>[\n\r\s]*$/i, '')

  const m = marked(markdown)

  return inline ? stripP(m) : m
}