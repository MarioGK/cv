<script lang='ts'>
  import marked from 'marked'

  let contentEl: Element
  let html = ''

  $: markdown = contentEl?.textContent || ''
  $: if (markdown) html = marked(markdown)
</script>

{#if html}
  {@html html}
{:else}
  <div style="display:none" bind:this={contentEl}><slot /></div>
{/if}