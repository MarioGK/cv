<script type='ts'>
  import { slide } from 'svelte/transition'
  import { mark } from '../../utils/markdown'
  
  import type { CurriculumAwardType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"
  
  export let awards: CurriculumAwardType[]
  
  let visible = true
</script>

{#if awards && awards.length > 0}
<section class="resume-section reference-section mb-3">
  <h2 on:click={() => visible = !visible} class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">Awards
    <Toggle {visible}/>
    <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
  </h2>
  {#if visible}
  <div class="resume-section-content" transition:slide>
    <ul class="list-unstyled resume-awards-list">
      {#each awards as award (award.title)}
      <li class="mb-2 pl-4 position-relative">
        <i class="resume-award-icon fas fa-trophy position-absolute" data-fa-transform="shrink-2"></i>
        <div class="resume-award-name">{ award.title }</div>
        <div class="resume-award-desc mb-1">{@html mark(award.description) }</div>
        {#if award.time}<div class="resume-degree-time">{ award.time }</div>{/if}
      </li>
      {/each}
    </ul>
  </div>
  {/if}
</section><!--//interests-section-->
{/if}