<script type='ts'>
  import { slide } from 'svelte/transition'

  import type { CurriculumExtraType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"

  export let extras: CurriculumExtraType[]

  let visibles = {}
  extras.forEach(extra => visibles[extra.title] = true)

</script>

{#if extras && extras.length > 0}
<section class="resume-section language-section mb-5">
  {#each extras as extra (extra.title)}
    <h2 on:click={() => visibles[extra.title] = !visibles[extra.title]} class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">{ extra.title }
      <Toggle visible={visibles[extra.title]}/>
      <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
    </h2>
    {#if visibles[extra.title]}
      <div class="resume-section-content" transition:slide>
        <ul class="list-unstyled resume-lang-list">
          {#each extra.items as item}
          <li class="mb-2">
            {#if item.title}<span class="resume-lang-name font-weight-bold">{ item.title }</span>{/if}
            {#if item.comment}<small class="text-muted font-weight-normal">{ item.comment }</small>{/if}
            {#if item.text}<div class="resume-award-desc">{ item.text }</div>{/if}
          </li>
          {/each}
        </ul>
      </div>
    {/if}
  {/each}
</section><!--//language-section-->
{/if}