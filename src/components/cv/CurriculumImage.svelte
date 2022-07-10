<script lang='ts'>
  import type { CurriculumImageType } from './Curriculum.type'

  export let img: CurriculumImageType
  const alt = img?.alt || img?.title.toLowerCase() || ''
  const title = img.title || img.alt || ''
</script>

{#if img.src}
  {#if img.href}
    <a href={img.href}>
      <img src={img.src} {alt} {title} >
    </a>
  {:else}
    <img src={img.src} {alt} {title} >
  {/if}
  {#if img.text}
    <small class="text-muted font-weight-normal">{img.text}</small>
  {/if}
{/if}
